package org.jbanana.rest;

public interface Realm {

	boolean checkPassword(String user, String password);
}