package org.jbanana.rest;

import org.jbanana.core.Command;
import org.jbanana.core.Container;
import io.vertx.ext.web.RoutingContext;

public interface HandlerInterceptor {

	void preCommandExecution(RoutingContext context, Container container, Command cmd);
	void posCommandExecution(RoutingContext context, Container container, Command cmd);
}