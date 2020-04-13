package org.jbanana.rest;

import static org.jbanana.xpath.XPath.addCollectionElement;
import static org.jbanana.xpath.XPath.clearCollection;
import static org.jbanana.xpath.XPath.deleteCollectionElementById;
import static org.jbanana.xpath.XPath.get;
import static org.jbanana.xpath.XPath.list;
import static org.jbanana.xpath.XPath.replaceCollectionElement;
import static org.jbanana.xpath.XPath.updateCollectionElement;
import java.io.Serializable;
import java.util.Date;
import org.jbanana.core.Persistent;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.RequiredArgsConstructor;

@AllArgsConstructor
@RequiredArgsConstructor
@SuppressWarnings("unchecked")
public class RestEntryPoint<T extends Persistent> {

	protected Serializable ps;
	protected Date now;

	protected final String collectionXPath;
	protected final String elementXPath;
		
	@Getter protected final String restContext;
	@Getter protected final Class<?> elementClass;
	
	public T create(T element, String...ids) {return addCollectionElement(ps, element, collectionXPath, ids);}

	public T[] restoreAll(String...ids) {return (T[])list(ps, elementClass, collectionXPath, ids);}
	public T restoreById(String...ids) {	return get(ps, elementXPath, ids);}
	
	public T update(T element, String...ids) {return updateCollectionElement(ps, element, elementXPath, ids);}
	public T replace(T element, String...ids) {return replaceCollectionElement(ps, element, elementXPath, ids);}
	
	public boolean deleteAll(String...ids) {clearCollection(ps, collectionXPath, ids); return true;}
	public T deleteById(String...ids) {return deleteCollectionElementById(ps, elementXPath, ids);}
}