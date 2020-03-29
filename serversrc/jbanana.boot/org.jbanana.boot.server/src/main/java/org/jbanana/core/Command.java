package org.jbanana.core;

import java.io.Serializable;
import java.lang.reflect.Constructor;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.Date;
import org.jbanana.core.Convetions.Crud;
import org.jbanana.rest.RestEntryPoint;
import org.prevayler.TransactionWithQuery;
import lombok.Data;

@Data
public class Command implements TransactionWithQuery {

	private static final long serialVersionUID = 1L;

	private final Class<?> parameterTypes[];
	private final Object parameterValues[];
	private final Class<?> controllerClass;
	private final Class<?> targetClass;
	private final String methodName;
	private final Crud crudMethod;
	
	private final String webContext;
	private final String elementXPath;
	private final String containerXPath;
	
	public Command(Object[] values, 
							  Class<?> controller, Class<?> target, 
							  Method method, Crud crud, String webContext,
							  String elementXPath, String containerXPath) {
		
		this.parameterValues = values;
		this.controllerClass = controller;
		this.targetClass = target;
		this.crudMethod = crud;
		this.webContext = webContext;
		this.elementXPath = elementXPath;
		this.containerXPath = containerXPath;
		this.methodName = method.getName();
		this.parameterTypes = method.getParameterTypes();
	}
	
	@Override
	public Object executeAndQuery(Object ps, Date now) throws Exception {
		
		Object key = ps;
		if(ps instanceof RootAndSequences)
			key = ((RootAndSequences)ps).getPrevalentSystem();
		
		Object controller = newController((Serializable) ps, now);
		Method method = controllerClass.getMethod(methodName, parameterTypes);
		if(crudMethod == Crud.create) {
			Container container = Container.getContainer(key);
			Identity identity = container.my(Identity.class);
			for (Object arg : parameterValues) {
				if(!(arg instanceof Persistent)) continue;
				if(((Persistent) arg).getId() !=null) continue;
				identity.setId(ps, (Persistent) arg);
			}
		}
		return method.invoke(controller, parameterValues);
	}

	@SuppressWarnings({ "unchecked", "rawtypes" })
	private Object newController(Serializable ps, Date now) throws NoSuchMethodException, InstantiationException, IllegalAccessException, InvocationTargetException {
		if(controllerClass!=RestEntryPoint.class){
			Constructor<?> constructor = controllerClass.getConstructor(new Class[]{Serializable.class, Date.class});
			return constructor.newInstance(new Object[]{ps, now});	
		} 
		
		return new RestEntryPoint(	ps, now, containerXPath, elementXPath, webContext, targetClass);
	}
}