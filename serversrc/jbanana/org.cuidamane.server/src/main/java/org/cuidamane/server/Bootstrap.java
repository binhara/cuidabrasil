package org.cuidamane.server;

import org.cuidamane.server.bo.Phonebook;
import org.cuidamane.server.interceptor.AuthorizerInteceptor;
import org.jbanana.JBananaBoot;
import org.jbanana.core.HandlerInterceptor;
import org.jbanana.core.PrevalentSystemInfo;

public class Bootstrap {

	public static void main(String[] args) throws Throwable {
		
		PrevalentSystemInfo phonebook = new PrevalentSystemInfo(new Phonebook());
		JBananaBoot.start(new HandlerInterceptor[] { new AuthorizerInteceptor() }, phonebook);
	}
}