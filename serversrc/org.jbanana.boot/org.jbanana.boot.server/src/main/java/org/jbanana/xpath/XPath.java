package org.jbanana.xpath;

import java.io.Serializable;
import java.lang.reflect.Array;
import java.lang.reflect.InvocationTargetException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

import org.apache.commons.beanutils.BeanUtilsBean;
import org.apache.commons.jxpath.ClassFunctions;
import org.apache.commons.jxpath.JXPathContext;
import org.jbanana.core.Persistent;
import org.jbanana.core.RootAndSequences;
import org.jbanana.exception.InfraRuntimeException;

import lombok.extern.slf4j.Slf4j;

@Slf4j
@SuppressWarnings("unchecked")
public abstract class XPath {
	
	public static void clearCollection(Serializable root, String xpath, String...ids) {
		clearCollection(root, xpath,  Arrays.asList(ids));
	}	
	
	public static void clearCollection(Serializable root, String xpath, Object...values) {
		clearCollection(root, xpath,  Arrays.asList(values));
	}
	
	private static void clearCollection(Serializable root, String xpath,  List<?> values) {
		Object data = getObject(root, xpath, values);
		
		if(data instanceof Collection)
			((Collection<?>) data).clear();
	
		if(data instanceof Map)
			((Map<?,?>) data).clear();
	}
	
	public static <T> T get(Object root, String xpath, String...ids) {
		return get(root, xpath, Arrays.asList(ids));
	}
	
	public static <T> T get(Object root, String xpath, Object...values) {
		return get(root, xpath, Arrays.asList(values));		
	}
	
	private static <T> T get(Object root,  String xpath, List<?> values) {
		return (T) getObject(root, xpath, values);
	}

	public static <T extends Persistent> T updateCollectionElement(Serializable root, T element, String xpath, Object...values) {
		values = (values==null || values.length==0) ? new Object[]{element.getId()} : values;
		return updateCollectionElement(root, element, xpath, Arrays.asList(values));
	}

	public static <T extends Persistent> T updateCollectionElement(Serializable root, T element, String xpath, String...ids) {
		ids = (ids==null || ids.length==0) ? new String[]{element.getId()} : ids;
		return updateCollectionElement(root, element, xpath, Arrays.asList(ids));
	}

	private static <T> T updateCollectionElement(Serializable root, T src, String xpath, List<?> values) {
		T target = getObject(root, xpath, values);
		try {
			new Updater().copyProperties(target, src);
			return target;
			
		} catch (Throwable e) {throw new InfraRuntimeException(e);}
	}
	
	public static <T extends Persistent> T addCollectionElement(Serializable root, T element, String xpath, String...ids) {
		return addCollectionElement(root, element, xpath, Arrays.asList(ids));
	}
	
	public static <T extends Persistent> T addCollectionElement(Serializable root, T element, String xpath, Object...values) {
		return addCollectionElement(root, element, xpath, Arrays.asList(values));
	}
	
	private static <T extends Persistent> T addCollectionElement(Serializable root, T element, String xpath, List<?> values) {
		if(element == null) return null;

		Object object = getObject(root, xpath, values);
		
		if(object == null){
			String xpathFormated= formatXPath(xpath, values);
			JXPathContext context = JXPathContext.newContext(root);
			context.setFunctions(new ClassFunctions(XPathFunctions.class, "xpath"));
			context.setValue(xpathFormated, element);
			return element;
		}

		if(object instanceof Collection){
			Collection<T> collection = (Collection<T>) object;
			collection.add(element);
			return element;
		}
		
		Map<String, T> map = (Map<String, T>) object;
		map.put(element.getId(), element);
		return element;
	}
	
	public static <T extends Persistent> T deleteCollectionElementById(Serializable ps, String xpath, String...ids) {

		T result = (T)getObject(ps, xpath, Arrays.asList(ids));
		String formatXPath = formatXPath(xpath,  Arrays.asList(ids));
		JXPathContext context = JXPathContext.newContext(ps);
		context.setFunctions(new ClassFunctions(XPathFunctions.class, "xpath"));
		context.removeAll(formatXPath);
		return result;
	}
	
	public static <T> T[] list(Serializable ps, Class<T> clazz, String xpath, String...ids) {
		return list(ps, clazz, xpath, Arrays.asList(ids));
	}

	public static <T> T[] list(Serializable ps, Class<T> clazz, String xpath, Object...values) {
		return list(ps, clazz, xpath, Arrays.asList(values));
	}

	private static <T> T[] list(Serializable ps, Class<T> clazz, String xpath, List<?> values) {
		
		String formatXPath = formatXPath(xpath, values);
		JXPathContext context = JXPathContext.newContext(ps);
		context.setFunctions(new ClassFunctions(XPathFunctions.class, "xpath"));
		
		Collection<Object> list = null;
		Object obj = null;
		
		try {
			obj = context.getValue(formatXPath);
			list = (obj instanceof Collection<?>) ? (Collection<Object>) obj
					: (obj instanceof Map<?, ?>) ?  ((Map<?, Object>) obj).values()
					: null;
			
		} catch (Throwable e) {
			log.info("XPath not found as a collection: " + xpath + ". Trying iterate...");
		}
		
		if(list==null){
			list = new ArrayList<>();
			Iterator<Object> iter = context.iterate(formatXPath);
			while (iter.hasNext()) 
				list.add(iter.next());
		}							
									
		T[] result = (T[]) Array.newInstance(clazz, list.size());
		result = list.toArray(result);
		return result;
	}	
	
	public static <T extends Persistent> T replaceCollectionElement(Serializable root, T element, String xpath, Object...values) {
		values = (values==null || values.length==0) ? new Object[]{element.getId()} : values;
		return replaceCollectionElement(root, element, xpath, Arrays.asList(values));
	}
	
	public static <T extends Persistent> T replaceCollectionElement(Serializable root, T element, String xpath, String...ids) {
		ids = (ids==null || ids.length==0) ? new String[]{element.getId()} : ids;
		return replaceCollectionElement(root, element, xpath, Arrays.asList(ids));
	}
	
	public static <T extends Persistent> T replaceCollectionElement(Serializable root, T element, String xpath, List<?> ids) {
		String xpathFormated = formatXPath(xpath, ids);
		JXPathContext context = JXPathContext.newContext(root);
		context.setFunctions(new ClassFunctions(XPathFunctions.class, "xpath"));
		context.setValue(xpathFormated, element);
		return element;
	}
	
	private static <T> T getObject(Object root, String xpath, List<?> ids) {
		if(root instanceof RootAndSequences) {
			root = ((RootAndSequences) root).getPrevalentSystem();
		}
		String xpathFormated = formatXPath(xpath, ids);
		JXPathContext context = JXPathContext.newContext(root);
		context.setFunctions(new ClassFunctions(XPathFunctions.class, "xpath"));
		return (T) context.getValue(xpathFormated);
	}	
	
	private static String formatXPath(String xpath, List<?> values) {
		
		if(values==null || values.size()==0) return xpath;
		
		String parts[] = xpath.split("\\?");
		StringBuilder buf = new StringBuilder();
		for (int i = 0; i < values.size(); i++) {
			buf.append(parts[i]);
			Object value = values.get(i);
			boolean isString = value instanceof String;
			if(isString) buf.append("'");
			buf.append(value);
			if(isString) buf.append("'");
		}
		
		buf.append(parts[values.size()]);
		return buf.toString();
	}
	
	public static class XPathFunctions{
		
		private static Map<String, Selector<?>> selectors = new HashMap<>();
		public static boolean like(String value, String like){
			
			String key = "like_" + like;
			if(!selectors.containsKey(key))
				selectors.put(key, new LikeSelector(like));
			
			LikeSelector selector = (LikeSelector) selectors.get(key);
			return selector.match(value);
		}
	}
}

class Updater extends BeanUtilsBean{

    @Override
    public void copyProperty(Object dest, String name, Object value) throws IllegalAccessException, InvocationTargetException {
        if(value==null)return;
        super.copyProperty(dest, name, value);
    }
}