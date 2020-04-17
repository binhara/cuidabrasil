package org.jbanana.web;

public interface Realm {

	boolean checkPassword(String user, String password);
}