package org.floripavrscorona.phonebook;

import org.floripavrscorona.phonebook.bo.Phonebook;
import org.jbanana.JBananaBoot;

public class Bootstrap {

	public static void main(String[] args) throws Throwable {
		
		JBananaBoot.start(new Phonebook());
	}
}