package org.jbanana.exception;

public class InfraRuntimeException extends RuntimeException {

	private static final long serialVersionUID = 1L;

	public InfraRuntimeException() {	super();}
	public InfraRuntimeException(String message) {super(message);}
	public InfraRuntimeException(Throwable cause) {super(cause);}
	public InfraRuntimeException(String message, Throwable cause) {super(message, cause);}
}