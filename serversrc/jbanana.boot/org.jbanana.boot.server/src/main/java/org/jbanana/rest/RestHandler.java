package org.jbanana.rest;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.Date;
import java.util.List;
import org.apache.commons.jxpath.JXPathNotFoundException;
import org.jbanana.core.Command;
import org.jbanana.core.Container;
import org.jbanana.core.Convetions.Crud;
import org.jbanana.core.Persistent;
import org.prevayler.Prevayler;

import io.vertx.core.Handler;
import io.vertx.core.http.HttpMethod;
import io.vertx.core.json.Json;
import io.vertx.ext.web.RoutingContext;
import lombok.AllArgsConstructor;
import lombok.extern.slf4j.Slf4j;

@Slf4j
@AllArgsConstructor
public class RestHandler implements Handler<RoutingContext> {

	private Container container;
	private RestMap mapping;
	private HandlerInterceptor[] interceptors;

	@Override
	public void handle(RoutingContext context) {
		
		Method method = mapping.getMethod();
		Class<?>[] types = method.getParameterTypes();
		
		if(types.length>0 && method.getDeclaringClass() == RestEntryPoint.class)
			fixParameterTypes(method, types);
			
		Object[] values = new Object[types.length];
		handleRequestParameters(context, mapping.getKeys(), types, values);

		try {

			boolean isGetMethod = context.request().method() == HttpMethod.GET;
			Command cmd = new Command(values, mapping.getEntryPointClass(), mapping.getTargetClass(), method, Crud.valueOf(mapping.getCrud()), 
															mapping.getContext(), mapping.getElementXPath(), mapping.getCollectionXPath());

			log.info("Path: " + mapping.getRoutablePath() + (isGetMethod ? ", Transient: " : ", Transactional: ") + method.getName()); 
			
			if(interceptors != null)
			{
				for (HandlerInterceptor interceptor : interceptors) {
					if(interceptor != null ) 
						interceptor.preCommandExecution(context, container, cmd);
					if(context.response().ended()) return;
				}
			}
			
			
			Prevayler prevayler = container.getPrevayler();
			Object result = isGetMethod 
					? cmd.executeAndQuery(container.getPrevalentSystem(), new Date(System.currentTimeMillis()))
					: prevayler.execute(cmd);
			
			if(interceptors != null)
			{
				for (HandlerInterceptor interceptor : interceptors) {
					if(interceptor != null ) 
						interceptor.posCommandExecution(context, container, cmd);
					if(context.response().ended()) return;
				}
			}
			
			context.response().putHeader("content-type", "application/json").end(Json.encodePrettily(result));
			
		} catch(InvocationTargetException e){ 
			
			handleInvocationTargetException(context, e);
		
		} catch (Throwable e) {	
			String msg = Json.encodePrettily(e.getCause().getMessage());
			context.response().putHeader("content-type", "application/json").end(msg);
			log.debug("****** Check this Throwable type: **************");
			e.printStackTrace();
		}
	}

	private void handleInvocationTargetException(RoutingContext context, InvocationTargetException e) {
		
		Throwable cause = e.getCause();
		String msg = Json.encodePrettily(cause.getMessage());
		if(cause instanceof JXPathNotFoundException || 
			cause instanceof ArrayIndexOutOfBoundsException){
			log.debug(msg);
			context.response()
						.putHeader("content-type", "application/json")
						.setStatusCode(204).end("{}");
			return;
		}

		context.response().putHeader("content-type", "application/json").end(msg);
		log.debug("****** Check this InvocationTargetException: **************");
		e.printStackTrace();
	}

	private void fixParameterTypes(Method method, Class<?>[] types) {
		
		Method methods[] = RestEntryPoint.class.getDeclaredMethods();
		for (Method m : methods) {
			if(!m.getName().equals(method.getName())) continue;
			for (int i = 0; i < types.length; i++) {
				if(types[i] != Persistent.class) continue;
				types[i] = mapping.getTargetClass();
			}
			return;
		}
	}

	private void handleRequestParameters(RoutingContext context, List<String> keys, Class<?>[] types, Object[] values) {

		Class<?> tp = types[0]; 
		if(tp != String[].class){
			String body = context.getBodyAsString();
			values[0] = decodeValue(tp, body);
		}
		
		String kv[] = new String[keys.size()];
		for (int i = 0; i < keys.size(); i++) 
			kv[i]  = context.request().getParam(keys.get(i));
		
		values[values.length-1] = kv;
	}

	private Object decodeValue(Class<?> clazz, String body) {
		if(body==null || body.isEmpty()) return null;
		return Json.decodeValue(body, clazz);
	}
}