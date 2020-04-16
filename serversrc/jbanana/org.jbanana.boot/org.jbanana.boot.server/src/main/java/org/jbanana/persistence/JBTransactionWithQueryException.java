package org.jbanana.persistence;

public class JBTransactionWithQueryException extends Exception {
	private static final long serialVersionUID = 1L;
	public JBTransactionWithQueryException(String message) {super(message);}
	public JBTransactionWithQueryException(Throwable cause) { super(cause);}
	public JBTransactionWithQueryException(String message, Throwable cause) {super(message, cause);}
}