package org.cuidamane.server.bo;

import java.io.Serializable;

import lombok.Data;

@Data
public class TokenAuthorization implements Serializable {

	private static final long serialVersionUID = 1L;
	
	private String token;
	private String idContact;
	
}
