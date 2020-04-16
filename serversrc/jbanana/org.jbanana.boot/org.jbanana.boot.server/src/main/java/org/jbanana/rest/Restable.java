package org.jbanana.rest;

public interface Restable {

	boolean avoidDelete();
	String xpathElement();
	String xpathContainer();
	String restContext();
}