package org.jbanana.exception;

public class ElementNotFoundException extends InfraRuntimeException {

	private static final long serialVersionUID = 1L;

	public ElementNotFoundException(String message) { super(message); }
	public ElementNotFoundException(String message, Throwable cause) { super(message, cause); }
}