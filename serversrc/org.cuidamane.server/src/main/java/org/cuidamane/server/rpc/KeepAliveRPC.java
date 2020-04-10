package org.cuidamane.server.rpc;

import java.util.Date;

import org.cuidamane.server.interceptor.Authorization;
import org.cuidamane.server.interceptor.AuthorizationType;
import org.jbanana.rpc.TransientRPC;
import org.jbanana.rpc.TransientRPC.HTTPMethod;

import io.vertx.ext.web.RoutingContext;

/**
 * @author Charles Buss
 */
@Authorization(type = AuthorizationType.None)
public class KeepAliveRPC {

	
	@TransientRPC(webContex = "/", method = HTTPMethod.GET) 
	public void keepAlive(RoutingContext context) {
		
		String response = " operating  - "+new Date().toString();
		
		context.response().setStatusCode(200).end(response);
		
	}
	
}