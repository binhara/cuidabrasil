package org.cuidamane.server.rpc;

import java.util.UUID;

import org.cuidamane.server.bo.Contact;
import org.cuidamane.server.bo.Phonebook;
import org.cuidamane.server.bo.RegisterPhoneManager;
import org.cuidamane.server.bo.TokenAuthorization;
import org.cuidamane.server.interceptor.Authorization;
import org.cuidamane.server.interceptor.AuthorizationType;
import org.cuidamane.util.BaseRPC;
import org.cuidamane.util.StatusCode;
import org.jbanana.persistence.JBTransactionWithQuery;
import org.jbanana.rpc.TransientRPC;
import org.jbanana.rpc.TransientRPC.HTTPMethod;

import io.vertx.core.json.Json;
import io.vertx.core.json.JsonObject;
import io.vertx.ext.web.RoutingContext;

@Authorization(type = AuthorizationType.None)
public class ValidateRegistrationRPC extends BaseRPC {

	private String  code    = null;
	private Contact contact = null;
	
	@TransientRPC(webContex = "/register-validation", method = HTTPMethod.POST) 
	public void registerToken(RoutingContext context) {
		this.setContext(context);
		
		if(!this.getRequestData()) return;
		if(!this.validateData())   return;
		
		Contact existentContact = Phonebook.getPrevalentSystem().getContacts().parallelStream()
			.filter(c ->c.getPhone().equals(this.contact.getPhone())).findFirst().orElse(null);
		
		if(existentContact != null)
			this.handleAlreadyExistentContact(existentContact);
		else
			this.handleNewContact(existentContact);

	}
	
	private void handleAlreadyExistentContact(Contact contact) {
		TokenAuthorization ta = Phonebook.getPrevalentSystem().getTokens().parallelStream()
			.filter(t->t.getIdContact().equals(contact.getId())).findFirst().orElse(null);
		if(ta == null) {
			ta = new TokenAuthorization();
			ta.setIdContact(contact.getId());
			ta.setToken(this.generateToken());
			
			try { 
				ta = JBTransactionWithQuery.create(Phonebook.class, ta, "tokens");
			} catch (Exception e) {
				this.setStatusCode(StatusCode.INTERNAL_SERVER_ERROR)
				.finishWithError("Some error occurred trying to save new contact.");
				return;
			}
		}else {
			ta.setToken(this.generateToken());
			try { 
				ta = JBTransactionWithQuery.update(Phonebook.class, ta, "tokens[@idContact=?]", ta.getIdContact());
			} catch (Exception e) {
				this.setStatusCode(StatusCode.INTERNAL_SERVER_ERROR)
				.finishWithError("Some error occurred trying to save new contact.");
				return;
			}	
		}
		
		this.setStatusCode(StatusCode.SUCCESS)
			.finish(new JsonObject().put("token", ta.getToken()));
	}
	
	private void handleNewContact(Contact contact) {
		//TODO create new Contact
		//TODO create TokenAuthorization
		this.setStatusCode(StatusCode.SERVICE_UNAVALIABLE).finish();
	}
	
	private String generateToken() {
		return UUID.randomUUID().toString();
	}
	
	private boolean validateData() {
		if(!RegisterPhoneManager.getInstance().hasValidPendinRegister(code, contact.getPhone())) {
			this.setStatusCode(StatusCode.BAD_REQUEST)
				.finishWithError("Code is invalid or expired.");
			return false;
		}
		RegisterPhoneManager.getInstance().removePendingRegister(code, contact.getPhone());
		return true;
	}
	
	private boolean getRequestData() {
		JsonObject json = null;
		try { json = this.getJsonObject();
		} catch(Exception e) {
			this.setStatusCode(StatusCode.BAD_REQUEST)
				.finishWithError("Error to parse json.");
			return false;
		}
		
		this.code = json.getString("code");
		if(code == null || code.isEmpty()) {
			this.setStatusCode(StatusCode.BAD_REQUEST)
				.finishWithError("code not sent.");
			return false;
		}
		
		try {
			this.contact = Json.decodeValue(json.getJsonObject("contact").encode(), Contact.class);
		}catch (Exception e) {
			e.printStackTrace();
			this.setStatusCode(StatusCode.BAD_REQUEST)
				.finishWithError("contact not sent or not valid.");
			return false;
		}
		
		return true;
	}
	
}
