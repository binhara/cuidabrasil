package org.cuidamane.util;

import java.util.HashMap;
import java.util.Map;

import io.vertx.core.MultiMap;
import io.vertx.core.json.Json;
import io.vertx.core.json.JsonObject;
import io.vertx.ext.web.RoutingContext;

/**
 * @author Charles Buss
 */
public abstract class BaseRPC {

	private   boolean 		 encodePrettily = false;
	protected RoutingContext context;
	protected MultiMap 		 headers;
	protected String   		 json;
	
	protected int responseStatusCode;
	protected Map<String,Object> responseJson = new HashMap<>();
	
	public BaseRPC setContext(RoutingContext context) {
		this.context = context;
		this.headers = context.request().headers();
		this.json    = context.getBodyAsString("UTF-8");
		return this;
	}
	protected RoutingContext getContext() { return context; }
	
	protected String getUrlParam(String key) { return context.request().getParam(key); }
	
	protected String getHeader(String key) { return headers.get(key); }
	
	public JsonObject getJsonObject() { 
		return new JsonObject(json); 
	}
	
	protected BaseRPC encodePrettily() {
		this.encodePrettily = true;
		return this;
	}
	
	protected BaseRPC setStatusCode(int statusCode) {
		this.responseStatusCode = statusCode;
		return this;
	}
	
	public BaseRPC addData(String key, Object value) {
		this.responseJson.put(key, value);
		return this;
	}
	
	public BaseRPC removeData(String key) {
		this.responseJson.remove(key);
		return this;
	}
	
	public void finish() {
		this.context.response().setStatusCode(this.responseStatusCode)
		.putHeader("Content-Type", "application/json")
		.putHeader("Cache-Control", "private");
		
		if(!responseJson.isEmpty())
		this.context.response()
		.end(encodePrettily ? Json.encodePrettily(responseJson) : Json.encode(responseJson));
	}
	
	public void finishWithError(String error) {
		JsonObject jsonError = new JsonObject().put("message", error);
		JsonObject body = new JsonObject().put("error", jsonError);
		
		this.context.response().setStatusCode(this.responseStatusCode)
		.putHeader("Content-Type", "application/json")
		.putHeader("Cache-Control", "private")
		.end(encodePrettily ? body.encodePrettily() : body.encode());
	}
	
	public void finish(JsonObject json) {
		this.context.response().setStatusCode(this.responseStatusCode)
		.putHeader("Content-Type", "application/json")
		.putHeader("Cache-Control", "private")
		.end(encodePrettily ? Json.encodePrettily(json) : Json.encode(json));
	}
		
}
