package com.mycompany.phonebook;

import org.jbanana.JBananaBoot;
import com.mycompany.phonebook.bo.Phonebook;

public class Server {

	public static void main(String[] args) throws Throwable {

		JBananaBoot.start(new Phonebook());
	}
}