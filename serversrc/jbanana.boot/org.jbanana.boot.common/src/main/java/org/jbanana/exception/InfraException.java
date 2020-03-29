package org.jbanana.exception;

public class InfraException extends Exception {

	private static final long serialVersionUID = 1L;

	public InfraException(String message) {super(message);}
	public InfraException(Throwable cause) { super(cause);}
	public InfraException(String message, Throwable cause) {super(message, cause);}
}