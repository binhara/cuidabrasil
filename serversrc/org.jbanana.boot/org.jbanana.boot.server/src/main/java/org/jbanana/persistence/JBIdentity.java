package org.jbanana.persistence;

import java.io.Serializable;

import org.jbanana.core.Identity;

public interface JBIdentity extends Serializable {
	public void setId(Object ps, Identity identity);
}
