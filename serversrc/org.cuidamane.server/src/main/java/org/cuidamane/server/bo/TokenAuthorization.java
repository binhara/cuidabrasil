package org.cuidamane.server.bo;

import org.jbanana.core.Persistent;

import lombok.Data;

@Data
public class TokenAuthorization implements Persistent {

	private static final long serialVersionUID = 1L;
	
	private String id;
	private String token;
	private String idContact;
	
	
}
