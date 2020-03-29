package org.cuidamane.server;

import org.cuidamane.server.bo.Phonebook;
import org.jbanana.JBananaBoot;

public class Bootstrap {

	public static void main(String[] args) throws Throwable {
		
		JBananaBoot.start(new Phonebook());
	}
}