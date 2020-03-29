package org.jbanana.rest;
import static org.apache.commons.lang3.StringUtils.leftPad;
import static org.apache.commons.lang3.StringUtils.rightPad;
import static org.jbanana.core.Convetions.PKG_KEY_BO;

import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import org.jbanana.core.Container;
import org.jbanana.core.Convetions.Crud;
import org.jbanana.core.Convetions.Rest;

import lombok.Data;

@Data
public class RestMap {
	
	private static final String REGEXP = "([^\\/]+)";
	
	private final String crud;
	private final String rest;
	private final String path;
	private final Class<?> targetClass;
	private final String entityName;
	private final Class<?> entryPointClass;
	private final Method method;
	
	private final boolean returnIsArray;	
	private final String splitablePath;
	private final String routablePath;
	private final List<String> keys;
	
	private final String context;
	private final String elementXPath;
	private final String collectionXPath;

	public RestMap(Container container, Crud crud, Rest rest, String path, Class<?> targetClass, Class<?> entryPointClass, Method method, String context, String elementXPath, String colectionXPath) {
		this.crud = crud.name();
		this.rest = rest.name();
		this.context = context;
		this.elementXPath = elementXPath;
		this.collectionXPath = colectionXPath;
		this.path = path.endsWith("/") ? path.substring(0, path.length()-1) : path;
		this.targetClass = targetClass;
		String pack = container.getConventions().get(PKG_KEY_BO);
		this.entityName = targetClass.getName().replace(pack + ".", "");
		this.entryPointClass = entryPointClass;
		this.method = method;
		this.returnIsArray = method.getReturnType().isArray();
		this.routablePath = initRoutablePath();
		this.splitablePath = routablePath.replace("*", REGEXP);
		this.keys = initKeys();
	}	
	
	@SuppressWarnings("unchecked")
	public List<String> initKeys(){
		String keys[] = path.split(":");
		if(keys.length==1) return Collections.EMPTY_LIST;
		
		List<String> result = new ArrayList<>();
		for (int i = 1; i < keys.length; i++) 
			result.add("param"+(i-1));

		return result;
	}
	
	private String initRoutablePath(){
		
		String keys[] = path.split(":");
		if(keys.length==1) return path;
		
		for (int i = 1; i < keys.length; i++) {
			String tmp = keys[i];
			int j = tmp.indexOf('/');
			j = j<0 ? tmp.length() : j;
			tmp = tmp.substring(0, j);
			keys[i] = keys[i].replaceFirst(tmp, "*");
		}
		
		StringBuilder buf = new StringBuilder();
		buf.append(keys[0]);
		for (int i = 1; i < keys.length; i++) 
			buf.append(keys[i]);
		
		return buf.toString();
	}
	
	public String resume() {
		return
		leftPad(targetClass==null ? entryPointClass.getSimpleName() 
												 : targetClass.getSimpleName(), 20) + " | " +
		rightPad(getMethod().getName() , 25) +
		rightPad(getCrud().toString() , 7) + " -> " +
		rightPad(getRest().toString() , 10) +
		rightPad("path args: " + getKeys() , 30) +
		"path: " + getRoutablePath();
	}
}