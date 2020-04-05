package org.cuidamane.server.interceptor;

import org.jbanana.core.Command;
import org.jbanana.core.Container;
import org.jbanana.core.HandlerInterceptor;

import io.vertx.ext.web.RoutingContext;

public class AuthorizerInteceptor implements HandlerInterceptor {

	
	
	
	@Override
	public void preCommandExecution(RoutingContext context, Container container, Command cmd) {
		
	}

	@Override
	public void posCommandExecution(RoutingContext context, Container container, Command cmd) { }

}
