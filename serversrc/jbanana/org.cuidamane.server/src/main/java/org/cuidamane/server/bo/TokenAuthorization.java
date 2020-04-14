package org.cuidamane.server.bo;

import org.jbanana.core.Persistent;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.experimental.Accessors;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Accessors(chain = true)
public class TokenAuthorization implements Persistent {

	private static final long serialVersionUID = 1L;
	
	private String id;
	private String token;
	private String idContact;
	private long   creationDate;
	private long   lastAccessDate;
		
}
