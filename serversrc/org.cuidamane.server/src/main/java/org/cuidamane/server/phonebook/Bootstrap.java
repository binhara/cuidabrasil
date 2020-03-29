package org.cuidamane.server.phonebook;

import org.cuidamane.server.phonebook.bo.Phonebook;
import org.jbanana.JBananaBoot;

public class Bootstrap {

	public static void main(String[] args) throws Throwable {
		
		JBananaBoot.start(new Phonebook());
	}
}