package org.jbanana.core;

import java.io.IOException;

import org.apache.http.client.fluent.Content;
import org.apache.http.client.fluent.Request;
import org.apache.http.entity.ContentType;

import com.fasterxml.jackson.annotation.JsonInclude.Include;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;

public abstract class Crud {
	
	@SuppressWarnings("unchecked")
	public static <T extends Persistent> T create(String baseUrl, T newObject) {
		try{
			String json = execute(Request.Post(baseUrl), toJson(newObject));
			return (T) new ObjectMapper().readValue(json, newObject.getClass());
		} catch (IOException e) {
			throw new RuntimeException(e);
		}		
	}
	
	public static String create(String baseURL, String json, boolean toDoubleQuotes, Object...args) {return create(baseURL, tryAppendValuesAndReplaceQuotes(json, toDoubleQuotes, args));}
	private static String create(String baseUrl, String json) { return execute(Request.Post(baseUrl), json);}

	@SuppressWarnings("unchecked")
	public static  <T extends Persistent> T  update(String baseUrl, T toUpdate) {
		try{
			String json = execute(Request.Put(baseUrl), toJson(toUpdate));
			return (T) new ObjectMapper().readValue(json, toUpdate.getClass());
		} catch (IOException e) {
			throw new RuntimeException(e);
		}
	}
	
	public static String update(String baseURL, String json, boolean toDoubleQuotes, Object...args) {return update(baseURL, tryAppendValuesAndReplaceQuotes(json, toDoubleQuotes, args));}
	private static String update(String baseUrl, String json) { return execute(Request.Put(baseUrl), json);}

	public static <T> T restore(String baseUrl, Class<T> clazz) {
		try {
			Content content = Request.Get(baseUrl).execute().returnContent();
			return new ObjectMapper().readValue(content.asString(), clazz);
		} catch (IOException e) {
			throw new RuntimeException(e);
		}
	}
	
	public static void delete(String url) {
		try {
			Request.Delete(url).execute().returnContent();
		} catch (IOException e) { throw new RuntimeException(e); }
	}
	
	private static String tryAppendValuesAndReplaceQuotes(String json,  boolean toDoubleQuotes, Object... args) {
		json = replaceQuotesIfNecessary(json, toDoubleQuotes);
		json = appendArgs(json, args);
		return json;
	}

	private static String replaceQuotesIfNecessary(String json, boolean toDoubleQuotes) {
		if(toDoubleQuotes) 
			return json.replaceAll("'", "\"");
		return json;
	}

	private static String appendArgs(String json, Object...args) {
		for (Object arg : args) 
			json = json.replaceFirst("\\{\\?\\}", ""+arg);
		return json;
	}
	
	public static String toJson(Object obj){
		try {
			ObjectMapper mapper = new ObjectMapper();
			mapper.setSerializationInclusion(Include.NON_NULL);
			mapper.setSerializationInclusion(Include.NON_EMPTY);
			return mapper.writeValueAsString(obj);
		} catch (JsonProcessingException e) {
			throw new RuntimeException(e);
		}
	}
	
	private static String execute(Request request, String json) {
		try {
			return request
					.bodyString(json, ContentType.DEFAULT_TEXT)
					.execute()
					.returnContent().toString();
		} catch (IOException e) {
			throw new RuntimeException(e);
		}
	}
}