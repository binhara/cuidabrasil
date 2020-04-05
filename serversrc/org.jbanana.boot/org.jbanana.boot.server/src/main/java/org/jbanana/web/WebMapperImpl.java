package org.jbanana.web;

import static org.jbanana.core.Convetions.DELETE_TRIGGER;
import static org.jbanana.core.Convetions.GET_TRIGGER;
import static org.jbanana.core.Convetions.PATCH_TRIGGER;
import static org.jbanana.core.Convetions.POST_TRIGGER;
import static org.jbanana.core.Convetions.PUT_TRIGGER;
import static org.jbanana.core.Convetions.Crud.create;
import static org.jbanana.core.Convetions.Crud.delete;
import static org.jbanana.core.Convetions.Crud.restore;
import static org.jbanana.core.Convetions.Crud.update;
import static org.jbanana.core.Convetions.Rest.get;
import static org.jbanana.core.Convetions.Rest.patch;
import static org.jbanana.core.Convetions.Rest.post;
import static org.jbanana.core.Convetions.Rest.put;

import java.io.Serializable;
import java.lang.reflect.Constructor;
import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.Date;
import java.util.List;

import org.jbanana.core.Container;
import org.jbanana.core.Convetions.Crud;
import org.jbanana.core.Convetions.Rest;
import org.jbanana.rest.EntryPoint;
import org.jbanana.rest.RestEntryPoint;
import org.jbanana.rest.RestMap;
import org.jbanana.rest.Restable;
import org.jbanana.rpc.RPCMap;
import org.jbanana.rpc.TransientRPC;
import org.jbanana.web.WebMapper;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.extern.slf4j.Slf4j;

@Slf4j
public class WebMapperImpl implements WebMapper {

	@Override
	public List<RestMap> inspectEntryPointsAndMap(Container container, Class<?>...classes) {
		List<RestMap> result = new ArrayList<>();
		for (int i = 0; i < classes.length; i++) 
			result.addAll(inspectEntryPointAndMap(container, classes[i]));
		return result;
	}

	@Override
	@SuppressWarnings("unchecked")
	public List<RestMap> inspectRestablesAndMap(Container container, Class<? extends Restable>... restables) {
		List<RestMap> result = new ArrayList<>();
		for (int i = 0; i < restables.length; i++) 
			result.addAll(inspectRestableAndMap(container, restables[i]));
		return result;
	}
	
	@Override
	public List<RestMap> inspectEntryPointAndMap(Container container, Class<?> clazz) {
		
		String restContext = null;
		Class<?> targetClass = null;
		
		EntryPoint rc = clazz.getAnnotation(EntryPoint.class);
		if(rc!=null) {
			restContext = rc.restContext();
			targetClass = rc.targetClass();
		}
			
		if(RestEntryPoint.class.isAssignableFrom(clazz)){
			EntryPointInfo info = readEntryPointInfoFromInstance(clazz);
			restContext = info.getContext();
			targetClass = info.getTargetClass();
		}
			
		if(restContext==null)	
			return Collections.emptyList();
		
		List<RestMap> result = new ArrayList<>();
		List<Method> methods = new ArrayList<>();
		findAllMethods(clazz, methods);
		
		String context = "/" + restContext + "/";
		context = context.replace("//", "/");
		findRestMethods(container, targetClass, clazz, methods, context, result, null, null, false);
		return result;
	}

	private EntryPointInfo readEntryPointInfoFromInstance(Class<?> clazz) {
		
		try {
			Constructor<?> c = clazz.getConstructor(new Class[]{Serializable.class, Date.class});
			RestEntryPoint<?> obj = (RestEntryPoint<?>) c.newInstance(new Object[]{null, null});
			return new EntryPointInfo(obj.getRestContext(), obj.getElementClass());
			
		} catch (Throwable e) {
			log.error(e.getMessage(), e);
			return null;
		}
	}
	
	private RestableInfo readRestableInfoFromInstance(Class<? extends Restable> clazz) {
		
		try {
			Constructor<?> c = clazz.getConstructor(new Class[0]);
			Restable obj = (Restable) c.newInstance(new Object[0]);
			return new RestableInfo(clazz, obj.restContext(), obj.xpathElement(), obj.xpathContainer(), obj.avoidDelete());
			
		} catch (Throwable e) {
			log.error(e.getMessage(), e);
			return null;
		}
	}	

	private void findAllMethods(Class<?> clazz, List<Method> methods) {
		methods.addAll(Arrays.asList(clazz.getDeclaredMethods()));
		if(clazz == Object.class) return;
		
		findAllMethods(clazz.getSuperclass(), methods);
	}

	private void findRestMethods(Container container, Class<?> target, Class<?> clazz, List<Method> methods, String context, List<RestMap> result, String elementXPath, String collectionXPath, boolean avoidDelete) {
		findRestMethods(container, result, create, post, POST_TRIGGER, context, target, clazz, methods, elementXPath, collectionXPath);
		findRestMethods(container, result, restore, get, GET_TRIGGER, context, target, clazz, methods, elementXPath, collectionXPath);
		findRestMethods(container, result, update, put, PUT_TRIGGER, context, target, clazz, methods, elementXPath, collectionXPath);
		findRestMethods(container, result, update, patch, PATCH_TRIGGER, context, target, clazz, methods, elementXPath, collectionXPath);
		
		if(avoidDelete) return;		
		findRestMethods(container, result, delete, Rest.delete, DELETE_TRIGGER, context, target, clazz, methods, elementXPath, collectionXPath);
	}

	private void findRestMethods(Container container, List<RestMap> result, Crud cm, Rest rm, String[] triggers, String context, Class<?> target, Class<?> clazz, List<Method> methods, String elementXPath, String collectionXPath) {
		for (Method method : methods) 
			findRestMethods(container, result, cm, rm, triggers, context, target, clazz, method, elementXPath, collectionXPath);
	}

	private void findRestMethods(Container container, List<RestMap> result, Crud cm, Rest rm, String[] triggers, String context, Class<?> target, Class<?> clazz, Method method, String elementXPath, String collectionXPath) {
		String name = method.getName();
		for (String trigger : triggers) {
			if(!name.startsWith(trigger)) continue;
			if(!name.equals(trigger)){
				log.debug(name);
				int index = name.indexOf("By");
				String by = name.substring(index<0 ? 0 : index+2);
				by = by.replace(trigger, "");
				by = by.toLowerCase();
				String path = "/" + context + "/" + by.toLowerCase() + "/" + ((index<0) ? "" : "/:" + by);
				path = path.replace("//", "/");
				path = path.replace("//", "/");
				result.add(new RestMap(container, cm, rm, path, target, clazz, method, context, elementXPath, collectionXPath));
				return;
			}	
			result.add(new RestMap(container, cm, rm, context, target, clazz, method, context, elementXPath, collectionXPath));
			return;
		}
	}

	@Override
	public List<RestMap> inspectRestableAndMap(Container container, Class<? extends Restable> targetClass) {
		
		if(targetClass.isInterface()) 
			return Collections.emptyList();
			
		RestableInfo info = readRestableInfoFromInstance(targetClass);
			
		if(info.getRestContext()==null)	
			return Collections.emptyList();
		
		List<RestMap> result = new ArrayList<>();
		List<Method> methods = new ArrayList<>();
		findAllMethods(RestEntryPoint.class, methods);
		
		String context = "/" + info.getRestContext() + "/";
		context = context.replace("//", "/");
		String xpathElement = info.getXpathElement();
		String xpathCollection = info.getXpathCollection();
		findRestMethods(container, targetClass, RestEntryPoint.class, methods, context, result, xpathElement, xpathCollection, info.isAvoidDelete());
		return result;
	}

	@Override
	public List<RPCMap> inspectRPCsAndMap(Container container, Class<?>... candidates) {
		
		List<RPCMap> result = new ArrayList<>();
		for (Class<?> candidate : candidates) 
			result.addAll(inspectRPCAndMap(container, candidate));
	
		return result;
	}

	@Override
	public List<RPCMap> inspectRPCAndMap(Container container, Class<?> candidate) {

		List<RPCMap> result = new ArrayList<>();
		
		Method[] methods = candidate.getDeclaredMethods();
		for (Method method : methods) {
			
			TransientRPC tr = method.getAnnotation(TransientRPC.class);
			if(tr==null) continue;
			
			RPCMap map = new RPCMap(container, candidate, method, tr.webContex(), tr.method().name().toUpperCase());
			result.add(map);
		}
		
		return result;
	}
}

@Data
@AllArgsConstructor
class EntryPointInfo{
	private final String context;
	private final Class<?> targetClass;
}

@Data
@AllArgsConstructor
class RestableInfo{
	private final Class<?> targetClass;
	private final String restContext;
	private final String xpathElement;
	private final String xpathCollection;
	private final boolean avoidDelete;
}