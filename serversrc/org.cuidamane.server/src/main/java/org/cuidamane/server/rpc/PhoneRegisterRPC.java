package org.cuidamane.server.rpc;

import org.cuidamane.server.bo.PendingRegister;
import org.cuidamane.server.bo.RegisterPhoneManager;
import org.cuidamane.server.interceptor.Authorization;
import org.cuidamane.server.interceptor.AuthorizationType;
import org.cuidamane.util.BaseRPC;
import org.cuidamane.util.StatusCode;
import org.jbanana.rpc.TransientRPC;
import org.jbanana.rpc.TransientRPC.HTTPMethod;

import io.vertx.core.json.JsonObject;
import io.vertx.ext.web.RoutingContext;

/**
 * @author Charles Buss
 */
@Authorization(type = AuthorizationType.None)
public class PhoneRegisterRPC extends BaseRPC {

	@TransientRPC(webContex = "/register", method = HTTPMethod.POST) 
	public void registerToken(RoutingContext context) {
		this.setContext(context);
		
		JsonObject json = null;
		try { json = this.getJsonObject();
		} catch(Exception e) {
			this.setStatusCode(StatusCode.BAD_REQUEST)
			.finishWithError("Error to parse json.");
			return;
		}
		
		String number = json.getString("phoneNumber");
		if(number == null || number.isEmpty()) {
			this.setStatusCode(StatusCode.BAD_REQUEST)
			.finishWithError("phoneNumber not sent.");
			return;
		}
		
		PendingRegister register = RegisterPhoneManager.getInstance().addToPendingRegisters(number);
		System.out.println(register.getCode());
		
		//TODO request to send SMS.
		
		this.setStatusCode(StatusCode.SUCCESS)
			.finish(new JsonObject().put("temp_code", register.getCode()));
	}
	
	
	
}
