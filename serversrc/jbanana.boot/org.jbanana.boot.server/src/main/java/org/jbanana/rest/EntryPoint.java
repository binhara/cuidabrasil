package org.jbanana.rest;

import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;

import org.jbanana.core.Persistent;

@Retention(RetentionPolicy.RUNTIME)
public @interface EntryPoint {
	
	String restContext();
	Class<? extends Persistent> targetClass();
}