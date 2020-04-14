package org.jbanana.rpc;

import static org.apache.commons.lang3.StringUtils.leftPad;
import static org.apache.commons.lang3.StringUtils.rightPad;

import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import org.jbanana.core.Container;

import lombok.Data;

@Data
public class RPCMap {
	
	private final Class<?> entryPointClass;
	private final Method method;
	private final String context;
	private final List<String> keys;
	private final String routablePath;
	private final String splitablePath;
	private final String httpMethod;

	public RPCMap(Container container,  Class<?> entryPointClass, Method method, String context, String httpMethod) {
		this.context = context;
		this.entryPointClass = entryPointClass;
		this.method = method;
		this.httpMethod = httpMethod;
		this.keys = initKeys();
		this.routablePath = initRoutablePath();
		this.splitablePath = routablePath.replace("?", "*");
	}	
	
	@SuppressWarnings("unchecked")
	public List<String> initKeys(){
		String keys[] = context.split(":");
		if(keys.length==1) return Collections.EMPTY_LIST;
		
		List<String> result = new ArrayList<>();
		for (int i = 1; i < keys.length; i++) 
			result.add("param"+(i-1));

		return result;
	}
	
	private String initRoutablePath(){
		
		String keys[] = context.split(":");
		if(keys.length==1) return context;
		
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
		leftPad(entryPointClass.getSimpleName(), 20) + " | " +
		rightPad(getMethod().getName() , 25) +
		rightPad("" , 7) + " -> " +
		rightPad(httpMethod.toLowerCase(), 10) +
		rightPad("path args: " + getKeys() , 30) +
		"path: " + getSplitablePath();
	}
}