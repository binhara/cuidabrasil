package org.jbanana.xpath;

import static org.jbanana.xpath.XPath.XPathFunctions.like;
import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertTrue;
import java.io.File;
import java.io.Serializable;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import org.jbanana.core.Container;
import org.jbanana.core.Persistent;
import org.jbanana.core.RootAndSequences;
import org.junit.Before;
import org.junit.Test;
import lombok.Data;

public class XPathTest {

	private RootMook root;
	private File testFolder = new File(".");
	
	@Before
	public void setUp() throws Exception {
		
		System.out.println(testFolder.getCanonicalPath());
		testFolder.delete();
		
		root = new RootMook();
		root.getElements().add(new ElementMook("1","listElement", "01"));
		root.getElements().add(new ElementMook("2","listElement", "02"));
		root.getElements().add(new ElementMook("3","Element", "03"));
		
		ElementMook map01 = new ElementMook("4","mapElement", "01");
		ElementMook map02 = new ElementMook("5","mapElement", "02");
		ElementMook map03 = new ElementMook("6","element", "03");
		
		root.getIndexById().put(map01.getId(), map01);
		root.getIndexById().put(map02.getId(), map02);
		root.getIndexById().put(map03.getId(), map03);

		Container c = new Container(root.getClass());
		c.initPrevayler(new RootAndSequences(root), testFolder);
	}

	@Test
	public void testList() {
		
		ElementMook[] elements;				

		elements = XPath.list(root, ElementMook.class, "elements[@pre=?]", "Element");
		assertEquals(1, elements.length);
		
		elements = XPath.list(root, ElementMook.class, "elements[@pre=?]", "listElement");
		assertEquals(2, elements.length);
	
		elements = XPath.list(root, ElementMook.class, "elements[xpath:like(@pre, ?)]", "*Element");
		assertEquals(3, elements.length);
		
		elements = XPath.list(root, ElementMook.class, "elements[@pos=?]", "01");
		assertEquals(1, elements.length);	
		
		elements = XPath.list(root, ElementMook.class, "elements");
		assertEquals(3, elements.length);
		
		ElementMook expected = elements[0];
		ElementMook candidate = XPath.deleteCollectionElementById(root, "elements[id=?]", expected.getId());
		assertEquals(expected, candidate);		

		elements = XPath.list(root, ElementMook.class, "elements");
		assertEquals(2, elements.length);
		
		ElementMook sub01 = new ElementMook("7","subElement", "01");
		ElementMook sub02 = new ElementMook("8","subElement", "02");
		ElementMook sub03 = new ElementMook("9","element", "03");
		
		String subelementXPath = "elements[id=?]/subelements[id=?]";
		String subelementsXPath = "elements[id=?]/subelements";

		String id = elements[0].getId();
		XPath.addCollectionElement(root, sub01, subelementsXPath, id);
		XPath.addCollectionElement(root, sub02, subelementsXPath, id);
		XPath.addCollectionElement(root, sub03, subelementsXPath, id);
		
		elements = XPath.list(root, ElementMook.class, subelementsXPath, id);
		assertEquals(3, elements.length);		
		
		ElementMook sub04 = new ElementMook(sub03.getId(), "xxxx", "04");
		candidate = XPath.updateCollectionElement(root, sub04, subelementXPath, id, sub03.getId());
		assertEquals(sub03, candidate);
		assertEquals("xxxx", sub03.getPre());
		assertEquals("04", sub03.getPos());
		
		sub04.setPos("pos");
		sub04.setPre("pre");
		candidate = XPath.updateCollectionElement(root, sub04, subelementXPath, (Object)id, sub03.getId());
		assertEquals(sub03, candidate);
		assertEquals("pre", sub03.getPre());
		assertEquals("pos", sub03.getPos());
		
		XPath.deleteCollectionElementById(root, subelementXPath, id, sub01.getId());
		elements = XPath.list(root, ElementMook.class, subelementsXPath, id);
		assertEquals(2, elements.length);
		
		candidate = XPath.get(root, subelementXPath, id, sub03.getId());
		assertEquals(sub03, candidate);
		
		candidate = XPath.get(root, subelementXPath, (Object)id, sub03.getId());
		assertEquals(sub03, candidate);
		
		XPath.clearCollection(root, subelementsXPath, id);
		elements = XPath.list(root, ElementMook.class, subelementsXPath, id);
		assertEquals(0, elements.length);
	}

	@Test
	public void testMap() {
		
		ElementMook[] elements = XPath.list(root, ElementMook.class, "indexById");
		assertEquals(3, elements.length);
		ElementMook expected = elements[0];
		
		ElementMook candidate = XPath.get(root, "indexById[@name=?]", expected.getId());
		assertEquals(expected, candidate);
		
		candidate = XPath.deleteCollectionElementById(root, "indexById[@name=?]", expected.getId());
		assertEquals(expected, candidate);		
		
		elements = XPath.list(root, ElementMook.class, "indexById");
		assertEquals(2, elements.length);
		
		ElementMook sub01 = new ElementMook("10","subElement", "01");
		ElementMook sub02 = new ElementMook("11","subElement", "02");
		ElementMook sub03 = new ElementMook("12","element", "03");
		
		String id = elements[0].getId();
		XPath.addCollectionElement(root, sub01, "indexById[@name=?]/subelements", id);
		XPath.addCollectionElement(root, sub02, "indexById[@name=?]/subelements", id);
		XPath.addCollectionElement(root, sub03, "indexById[@name=?]/subelements", (Object)id);
		
		elements = XPath.list(root, ElementMook.class, "indexById[@name=?]/subelements", id);
		assertEquals(3, elements.length);
		
		XPath.deleteCollectionElementById(root, "indexById[@name=?]/subelements[id=?]", id, sub01.getId());
		elements = XPath.list(root, ElementMook.class, "indexById[@name=?]/subelements", id);
		assertEquals(2, elements.length);
		
		XPath.clearCollection(root, "indexById[@name=?]/subelements", (Object)id);
		elements = XPath.list(root, ElementMook.class, "indexById[@name=?]/subelements", id);
		assertEquals(0, elements.length);		
	}	
	
	@Test
	public void testLike() {
		assertTrue(like("a", "a"));
		assertTrue(like("abcde", "a*"));
		assertTrue(like("abcde", "*c*"));
		assertTrue(like("abcde", "*e"));
		
		assertFalse(like("abcde", "a"));
		assertFalse(like("abcde", "*f*"));
		assertFalse(like("abcde", "*a"));
		assertFalse(like("abcde", "e*"));
	}

	@Data
	public static class RootMook implements Serializable{
		
		private static final long serialVersionUID = 1L;
		
		List<ElementMook> elements = new ArrayList<>();
		Map<String, ElementMook> indexById = new HashMap<>();
	}
	
	@Data
	public static class ElementMook implements Persistent{
		
		private static final long serialVersionUID = 1L;
		
		private final String id;
		private String pre;
		private String pos;
		
		List<ElementMook> subelements = new ArrayList<>();
		
		public ElementMook(String id, String pre, String pos) {
			this.id = id;
			this.pre = pre;
			this.pos = pos;
		}
		
		public String getFull(){	return pre + " " + pos;}
	}
}