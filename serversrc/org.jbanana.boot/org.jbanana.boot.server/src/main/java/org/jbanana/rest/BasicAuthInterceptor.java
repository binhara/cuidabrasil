package org.jbanana.rest;

import java.util.HashMap;
import java.util.Map;
import java.util.UUID;
import org.jbanana.core.Command;
import org.jbanana.core.Container;
import org.jbanana.core.HandlerInterceptor;

import io.vertx.ext.web.RoutingContext;
import lombok.AllArgsConstructor;

@AllArgsConstructor
public class BasicAuthInterceptor implements HandlerInterceptor{
	
	private static final String JBANANA_TK_KEY = "jbananaTk";
	private static final long TOKEN_EXPIRATION_MILIS = 5 * 60 * 1000; //5 min
	private static final UUID uuid = UUID.randomUUID();
	private static final Map<String, Long> _tokens = new HashMap<>();
	
	private final Realm realm;
	
	@Override
	public void preCommandExecution(RoutingContext context, Container container, Command cmd) {
		
		String token = context.request().getHeader(JBANANA_TK_KEY); 
		if(checkToken(token)){
			return;
		}
				
		
		
		if(token != null && context.request().getHeader("user") == null)
		{
			context.response().setStatusCode(408).end();
			return;
		}
		
		if(!checkLogin(context)) {
			context.response().setStatusCode(401).end();	
			return;
		}
	}

	@Override
	public void posCommandExecution(RoutingContext context, Container container, Command cmd) {}
	
	private boolean checkLogin(RoutingContext context) {
		
		String user = context.request().getHeader("user"); //alterado getParam
		String password = context.request().getHeader("password"); //alterado getParam
		
		if(!realm.checkPassword(user, password)) 
			return false;
		
		String token = ""+uuid.getMostSignificantBits();
		_tokens.put(token,  System.currentTimeMillis());
		
		context.response().putHeader(JBANANA_TK_KEY, token);
		return true;
	}

	private boolean checkToken(String token) {
		
		if(!_tokens.containsKey(token)) 
			return false;
		
		Long t0  = _tokens.get(token);
		if(t0.longValue() + TOKEN_EXPIRATION_MILIS > System.currentTimeMillis()){
			_tokens.remove(token);
			return false;
		}
			
		_tokens.put(token,  System.currentTimeMillis());
		
		return true;
	}
}