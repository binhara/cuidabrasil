package org.jbanana.rpc;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;

import org.apache.commons.jxpath.JXPathNotFoundException;
import org.jbanana.core.Container;
import org.jbanana.log.Logger;
import org.jbanana.log.StackTraceUtil;
import org.jbanana.rpc.RPCHandlerInterceptor;

import io.vertx.core.Handler;
import io.vertx.core.json.Json;
import io.vertx.ext.web.RoutingContext;
import lombok.AllArgsConstructor;
import lombok.extern.slf4j.Slf4j;

@Slf4j
@AllArgsConstructor
public class RPCHandler implements Handler<RoutingContext> {

	private static final String TAG = "[RPC HANDLER] - ";
	
	private Container container;
	private RPCMap mapping;
	private RPCHandlerInterceptor[] interceptors;

	@Override
	public void handle(RoutingContext context) {
		
		Class<?> clazz  = mapping.getEntryPointClass();
		Method   method = mapping.getMethod();
		Logger.info(TAG+"path=" + mapping.getRoutablePath() + ", RPC=" + method.getName()); 

		try {
			
			long start = System.nanoTime();
			if(interceptors != null) {
				for (RPCHandlerInterceptor interceptor : interceptors) {
					if(interceptor != null ) 
						interceptor.preCommandExecution(context, container, clazz);
					if(context.response().ended()) return;
				}
			}
			Logger.info(TAG+"path=" + mapping.getRoutablePath()+", interceptorLatency="+((System.nanoTime()-start) / 1000000000.0));
			method.invoke(clazz.getConstructor()
									.newInstance(), 
									new Object[] {context});
			Logger.info(TAG+"path=" + mapping.getRoutablePath()+", statusCode="+context.response().getStatusCode()
					   +", bodySizeBytes="+context.response().bytesWritten()+", executionLatency="+((System.nanoTime()-start) / 1000000000.0));
			
			
			if(interceptors != null) {
				for (RPCHandlerInterceptor interceptor : interceptors) {
					if(interceptor != null ) 
						interceptor.posCommandExecution(context, container, clazz);
					if(context.response().ended()) return;
				}
			}		
			
		} catch(InvocationTargetException e){ 			
			handleInvocationTargetException(context, e);
		
		} catch (Throwable e) {	
			String message = (e.getCause() != null ) ? e.getCause().getMessage() : e.getMessage();
			String msg = Json.encodePrettily(message);
			context.response().putHeader("content-type", "application/json").end(msg);
			Logger.error(TAG+"****** Check this Throwable type: **************");
			Logger.exception(TAG, e);
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

		context.response().putHeader("content-type", "application/json").setStatusCode(500).end(msg);
		Logger.error("****** Check this InvocationTargetException: **************");
		Logger.error(e.getMessage());
		Logger.error(StackTraceUtil.getStackTrace(e));
	}
}