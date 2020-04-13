package org.jbanana.rpc;

import org.jbanana.core.Command;
import org.jbanana.core.Container;
import org.jbanana.core.HandlerInterceptor;

import io.vertx.ext.web.RoutingContext;

public interface RestHandlerInterceptor extends HandlerInterceptor {

	void preCommandExecution(RoutingContext context, Container container, Command cmd);
	void posCommandExecution(RoutingContext context, Container container, Command cmd);
}