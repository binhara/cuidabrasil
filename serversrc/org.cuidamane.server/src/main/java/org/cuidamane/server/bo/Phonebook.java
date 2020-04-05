package org.cuidamane.server.bo;

import java.io.Serializable;
import java.util.HashSet;
import java.util.Set;

import org.jbanana.core.Container;

import lombok.Getter;
import lombok.NoArgsConstructor;

@NoArgsConstructor
public class Phonebook implements Serializable {

	private static final long serialVersionUID = 1L;
	
	@Getter private final Set<Contact> 			  contacts = new HashSet<>();
	@Getter private final Set<TokenAuthorization> tokens   = new HashSet<>();
	

	public static Phonebook getPrevalentSystem() {
		return (Phonebook)Container.getInstances().get(Phonebook.class.getSimpleName()).getPrevalentSystem();
	}
	
}