package org.jbanana.rpc;

import org.jbanana.core.Container;
import org.jbanana.core.HandlerInterceptor;

import io.vertx.ext.web.RoutingContext;

public interface RPCHandlerInterceptor extends HandlerInterceptor {

	void preCommandExecution(RoutingContext context, Container container, Class<?> cmd);
	void posCommandExecution(RoutingContext context, Container container, Class<?> cmd);
}