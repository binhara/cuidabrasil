package org.jbanana.persistence;

import java.io.Serializable;
import java.util.Date;
import java.util.List;

import org.jbanana.core.Container;
import org.jbanana.core.Identity;
import org.jbanana.core.Persistent;
import org.jbanana.core.RootAndSequences;
import org.jbanana.rpc.TransientRPC.HTTPMethod;
import org.jbanana.xpath.XPath;
import org.prevayler.TransactionWithQuery;

public class JBTransactionWithQuery implements TransactionWithQuery {

	private static final long serialVersionUID = 1L;
	
	private Serializable serializable;
	private Persistent   element;
	private HTTPMethod   method;
	private Identity     identity;
	private String[]     ids;
	private String       xpath;
	private Object       ps;
	
	protected JBTransactionWithQuery(Persistent element, String xpath, String[] ids, HTTPMethod method) {
		this.element    = element;
		this.method     = method;
		this.xpath      = xpath;
		this.ids        = ids;
	}
	
	// ------------------------------------------------- CREATE -------------------------------------------------
	/**
	 * Create and add the Persistent object to the collection using XPath
	 * @param root class of Serializable
	 * @param element
	 * @param xpath
	 * @return "<T extends Persistent> T" object created.
	 * @throws Exception
	 */
	public static <T extends Persistent> T create(Class<?> root, Persistent element, String xpath) throws Exception {
		return create(root, element, xpath, new String[0]);
	}
	/**
	 * Create and add the Persistent object to the collection on XPath
	 * @param root class of Serializable
	 * @param element
	 * @param xpath
	 * @param ids
	 * @return "<T extends Persistent> T" object created.
	 * @throws Exception
	 */
	public static <T extends Persistent> T create(Class<?> root, Persistent element, String xpath, List<String> ids) throws Exception {
		return create(root, element, xpath, ids != null ? ids.toArray(new String[ids.size()]) : null);
	}
	/**
	 * Create and add the Persistent object to the collection on XPath
	 * @param root class of Serializable
	 * @param element
	 * @param xpath
	 * @param ids
	 * @return "<T extends Persistent> T" object created.
	 * @throws Exception
	 */
	@SuppressWarnings("unchecked")
	public static <T extends Persistent> T create(Class<?> root, Persistent element, String xpath, String...ids) throws Exception {
		return (T) new JBTransactionWithQuery(element, xpath, prepareIdList(ids), HTTPMethod.POST).executePrevayler(root);
	}
	private Object post() {
		this.setId();
		return (ids == null || ids.length <= 0) ?
				XPath.addCollectionElement(serializable, element, xpath) :	
				XPath.addCollectionElement(serializable, element, xpath, ids);
	}

	// ------------------------------------------------- REPLACE -------------------------------------------------
	/**
	 * Replace the Persistent object existent in a collection using XPath
	 * @param root class of Serializable 
	 * @param element
	 * @param xpath
	 * @return "<T extends Persistent> T" object created.
	 * @throws Exception
	 */
	public static <T extends Persistent> T replace(Class<?> root, Persistent element, String xpath) throws Exception {
		return replace(root, element, xpath, new String[0]);
	}
	/**
	 * Replace the Persistent object existent in a collection using XPath
	 * @param root class of Serializable
	 * @param element
	 * @param xpath
	 * @param ids
	 * @return "<T extends Persistent> T" object created.
	 * @throws Exception
	 */
	public static <T extends Persistent> T replace(Class<?> root, Persistent element, String xpath, List<String> ids) throws Exception {
		return replace(root, element, xpath, (ids == null ? null: ids.toArray(new String[ids.size()])));
	}
	/**
	 * Replace the Persistent object existent in a collection using XPath
	 * @param root class of Serializable
	 * @param element
	 * @param xpath
	 * @param ids
	 * @return "<T extends Persistent> T" object created.
	 * @throws Exception
	 */
	@SuppressWarnings("unchecked")
	public static <T extends Persistent> T replace(Class<?> root, Persistent element, String xpath, String...ids) throws Exception {
		return (T) new JBTransactionWithQuery(element, xpath, prepareIdList(ids), HTTPMethod.PUT).executePrevayler(root);
	}
	private Object put() {
		this.setId();
		return (ids == null || ids.length <= 0) ?
				XPath.replaceCollectionElement(serializable, element, xpath) :
			    XPath.replaceCollectionElement(serializable, element, xpath, ids);
	}
	
	// ------------------------------------------------- UPDATE -------------------------------------------------
	/**
	 * Update the Persistent object existent in a collection using XPath
	 * @param root class of Serializable 
	 * @param element
	 * @param xpath
	 * @return "<T extends Persistent> T" object created.
	 * @throws Exception
	 */
	public static <T extends Persistent> T update(Class<?> root, Persistent element, String xpath) throws Exception {
		return update(root, element, xpath, new String[0]);
	}
	/**
	 * Update the Persistent object existent in a collection using XPath
	 * @param root class of Serializable 
	 * @param element
	 * @param xpath
	 * @param ids
	 * @return "<T extends Persistent> T" object created.
	 * @throws Exception
	 */
	public static <T extends Persistent> T update(Class<?> root, Persistent element, String xpath, List<String> ids) throws Exception {
		return update(root, element, xpath, (ids == null ? null: ids.toArray(new String[ids.size()])));
	}
	/**
	 * Update the Persistent object existent in a collection using XPath
	 * @param root class of Serializable 
	 * @param element
	 * @param xpath
	 * @param ids
	 * @return "<T extends Persistent> T" object created.
	 * @throws Exception
	 */
	@SuppressWarnings("unchecked")
	public static <T extends Persistent> T update(Class<?> root, Persistent element, String xpath, String...ids) throws Exception {
		return (T) new JBTransactionWithQuery(element, xpath, prepareIdList(ids), HTTPMethod.PATCH).executePrevayler(root);
	}
	private Object patch() {
		this.setId();
		return (ids == null || ids.length <= 0) ?
				XPath.updateCollectionElement(serializable, element, xpath) :
			    XPath.updateCollectionElement(serializable, element, xpath, ids);
	}
	
	// ------------------------------------------------- DELETE -------------------------------------------------
	/**
	 * Delete the Persistent object existent in a collection using XPath
	 * @param root class of Serializable 
	 * @param xpath
	 * @param ids
	 * @return "<T extends Persistent> T" object created.
	 * @throws Exception
	 */
	public static <T extends Persistent> T delete(Class<?> root, String xpath, String id) throws Exception { return delete(root, xpath, new String[] {id}); }
	/**
	 * Delete the Persistent object existent in a collection using XPath
	 * @param root class of Serializable 
	 * @param xpath
	 * @param ids
	 * @return "<T extends Persistent> T" object created.
	 * @throws Exception
	 */
	public static <T extends Persistent> T delete(Class<?> root, String xpath, List<String> ids) throws Exception {
		return delete(root, xpath, (ids == null ? null: ids.toArray(new String[ids.size()])));
	}
	/**
	 * Delete the Persistent object existent in a collection using XPath
	 * @param root class of Serializable 
	 * @param xpath
	 * @param ids
	 * @return "<T extends Persistent> T" object created.
	 * @throws Exception
	 */
	@SuppressWarnings("unchecked")
	public static <T extends Persistent> T delete(Class<?> root, String xpath, String...ids) throws Exception {
		return (T) new JBTransactionWithQuery(null, xpath, prepareIdList(ids), HTTPMethod.DELETE).executePrevayler(root);
	}
	private Object delete() {
		return (ids == null || ids.length <= 0) ?
				XPath.deleteCollectionElementById(serializable, xpath) :
			    XPath.deleteCollectionElementById(serializable, xpath, ids);
	}
	
	
	private Object executePrevayler(Class<?> clazz) throws Exception {
		return Container.getInstances().get(clazz.getSimpleName()).getPrevayler().execute(this);
	}
	
	/**
	 * Prepare the array of Strings to the Xpath
	 * @param String[] ids
	 * @return ids == null ? null : ids.length == 0 ? null : ids;
	 */
	private static String[] prepareIdList(String[] ids) { return ids == null ? null : ids.length == 0 ? null : ids; }
	
	/**
	 * Set id to the Persistent object if id is null or empty
	 */
	private void setId() {
		if(element instanceof JBIdentity)
			((JBIdentity)element).setId(ps, identity);
		else if(element.getId() == null || element.getId().isEmpty())
				identity.setId(ps, element);
	}
	
	@Override public Object executeAndQuery(Object ps, Date now) throws Exception {
		this.ps = ps;
		this.serializable = ((RootAndSequences)ps).getPrevalentSystem();
		this.identity     = Container.getContainer(serializable).my(Identity.class);
		switch (method) {
			case DELETE:
				return this.delete();
			case PATCH:
				return this.patch();
			case POST:
				return this.post();				
			case PUT:
				return this.put();
			default:
				throw new JBTransactionWithQueryException("Any method set as target.");
		}
	}
}
