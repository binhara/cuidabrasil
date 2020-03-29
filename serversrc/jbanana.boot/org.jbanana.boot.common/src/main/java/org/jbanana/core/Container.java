package org.jbanana.core;

import java.io.File;
import java.io.Serializable;
import java.util.HashMap;
import java.util.Map;

import org.jbanana.core.Convetions.Singleton;
import org.jbanana.exception.InfraRuntimeException;
import org.prevayler.Prevayler;
import org.prevayler.PrevaylerFactory;

import lombok.Getter;
import lombok.Synchronized;
import lombok.extern.slf4j.Slf4j;

@Slf4j
public class Container {
	
	@Getter
	private final static Map<String, Container> instances = new HashMap<>();

	@Getter
	private String alias;
	
	
	public Container(Class<?> clazz){
		this.alias = clazz.getSimpleName();
		instances.put(alias, this);
	}
	
	public static Container getContainer(RootAndSequences rs) {	
		return getContainer(rs.getPrevalentSystem().getClass());
	}
	
	
	public static Container getContainer(Class<?> psClass) {		
		String name = psClass.getSimpleName();
		return instances.get(name);
	}
	
	@Getter
	private Prevayler prevayler;

	@Getter
	private final Map<String, String> conventions = new HashMap<>();
	private final Map<Class<?>, Object> singletons = new HashMap<>();

	public void initPrevayler(RootAndSequences ras) {initPrevayler(ras, null);}	
	public void initPrevayler(RootAndSequences ps, File folder) {
		try {
			if(folder==null){
				prevayler = PrevaylerFactory.createPrevayler(ps, new File(alias).getCanonicalPath());
				return;
			}
			prevayler = PrevaylerFactory.createPrevayler(ps, new File(folder.getCanonicalPath(), alias).getCanonicalPath());
			
		} catch (Throwable e) {throw new RuntimeException("Fail to init prevayler!", e);}
	}	
	
	public Serializable getPrevalentSystem() {
		if(prevayler==null) return null;
		RootAndSequences ras = (RootAndSequences) prevayler.prevalentSystem();
		return ras.getPrevalentSystem();
	}	
	
	@Synchronized
	public void clear() {
		singletons.clear();
		conventions.clear();
	}	
	
	@Synchronized
	public void registry(Object component) {
		
		Class<?> key = findSingletonInterface(component.getClass());
		if (!key.isInterface())
			throw new InfraRuntimeException(key.getName() + " is not a Interface!");

		singletons.put(key, component);
	}

	public void registry(String conventionKey, String value) { registry(conventionKey, value, true);}
	public void registry(String conventionKey, String value, boolean override) {
		
		log.debug(conventionKey + " =: '" + value + "'");
		if (conventions.containsKey(conventionKey) && !override) return;

		conventions.put(conventionKey, value);
	}

	public String my(String conventionKey) {
		return conventions.get(conventionKey);
	}
	
	@SuppressWarnings("unchecked")
	public <T> T my(Class<T> clazz) {

		if (singletons.containsKey(clazz))
			return (T) singletons.get(clazz);

		try {
			return instantiateAndRegistry(clazz);
		} catch (Throwable e) {
			throw new InfraRuntimeException(e);
		}
	}

	@SuppressWarnings("unchecked")
	private <T> T instantiateAndRegistry(Class<T> clazz)
			throws ClassNotFoundException, InstantiationException, IllegalAccessException {

		Class<?> key;
		if (clazz.isInterface()) {
			if (clazz.getAnnotation(Singleton.class) == null)
				throw new InfraRuntimeException(clazz.getName() + " is not a @Singleton!");

			key = clazz;
			clazz = (Class<T>) Class.forName(clazz.getName() + "Impl");
			return instantiate(clazz, key);
		} 
		
		key = findSingletonInterface(clazz);
		return instantiate(clazz, key);
	}

	private <T> T instantiate(Class<T> clazz, Class<?> key) throws InstantiationException, IllegalAccessException {
		T result = clazz.newInstance();
		singletons.put(key, result);
		return result;
	}

	private static Class<?> findSingletonInterface(Class<?> clazz) {
		Class<?>[] interfaces = clazz.getInterfaces();
		for (Class<?> candidate : interfaces)
			if (candidate.getAnnotation(Singleton.class) != null)
				return candidate;

		throw new InfraRuntimeException("none interface with annotation @Singleton founded (" + clazz.getName() + ")");
	}
}