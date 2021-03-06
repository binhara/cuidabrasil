package org.cuidamane.server.interceptor;

import org.cuidamane.server.bo.Phonebook;
import org.cuidamane.server.bo.TokenAuthorization;
import org.cuidamane.util.StatusCode;
import org.jbanana.core.Command;
import org.jbanana.core.Container;
import org.jbanana.log.Logger;
import org.jbanana.rpc.RPCHandlerInterceptor;
import org.jbanana.rpc.RestHandlerInterceptor;

import io.vertx.ext.web.RoutingContext;

/**
 * @author Charles Buss
 */
public class AuthorizerInteceptor implements RestHandlerInterceptor, RPCHandlerInterceptor {

	private static final String TAG = "[AUTHORIZER] -";
	
	private static final String K_API_TOKEN = "j-api-key";
	private static String API_TOKEN = null;
	
 	private void preCommandExecutionForAll(RoutingContext context, Container container, Command cmd, Class<?>clazz_cmd) {		
		Authorization clazz_authorization = clazz_cmd.getAnnotation(Authorization.class);
		if(clazz_authorization == null) {
			Logger.warning(TAG+clazz_cmd+" not contains Authorization annotation.");
			return;
		}
		
		switch (clazz_authorization.type()) {
			case None:
				Logger.info(TAG+"AuthorizationType="+AuthorizationType.None);
				return;
			case Token:
				Logger.info(TAG+"AuthorizationType="+AuthorizationType.Token);
				tokenHandler(context);
				return;
			case Api:
				Logger.info(TAG+"AuthorizationType="+AuthorizationType.Api);
				apiHandler(context);
				return;
		}
	}
	
	private void tokenHandler(RoutingContext context) {
		String token = context.request().getHeader("token");
		if(token == null || token.isEmpty()) {
			Logger.warning("Request refused token not sent.");
			context.response()
				.setStatusCode(StatusCode.UNAUTHORIZED)
				.end();
			return;
		}
			
		TokenAuthorization ta = Phonebook.getPrevalentSystem().getTokens().parallelStream()
				.filter(t -> t.getToken().equals(token)).findFirst().orElse(null);
	
		if(ta == null) {
			Logger.warning("Request refused token is not valid.");
			context.response()
				.setStatusCode(StatusCode.UNAUTHORIZED)
				.end();
			return;
		}
		
		//authorized
		ta.setLastAccessDate(System.currentTimeMillis());
	}
	
	private void apiHandler(RoutingContext context) {
		
		if(API_TOKEN == null) return;
	
		String jToken = context.request().getHeader(K_API_TOKEN);
		if(jToken == null || jToken.isEmpty()) {
			Logger.warning("Request refused "+K_API_TOKEN+" not sent.");
			context.response()
				.setStatusCode(StatusCode.UNAUTHORIZED)
				.end();
			return;
		}
		
		if(!jToken.equals(API_TOKEN)) {
			Logger.warning("Request refused "+K_API_TOKEN+" is invalid, value="+jToken);
			context.response()
				.setStatusCode(StatusCode.UNAUTHORIZED)
				.end();
			return;
		}
		
		return;
	}
	
	@Override
	public void preCommandExecution(RoutingContext context, Container container, Command cmd) {
		this.preCommandExecutionForAll(context, container, cmd, cmd.getTargetClass());
	}

	@Override
	public void posCommandExecution(RoutingContext context, Container container, Command cmd) { }

	@Override
	public void preCommandExecution(RoutingContext context, Container container, Class<?> cmd) { 
		this.preCommandExecutionForAll(context, container, null, cmd);
	}

	@Override
	public void posCommandExecution(RoutingContext context, Container container, Class<?> cmd) { }

}
