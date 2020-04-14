package org.jbanana.rpc;

import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;

@Retention(RetentionPolicy.RUNTIME)
public @interface TransientRPC {
	
	public static enum HTTPMethod {
		GET, POST, PUT, PATCH, DELETE, HEAD
	}
	
	String webContex();
	HTTPMethod method();	
}