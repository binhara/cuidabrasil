package org.jbanana.core;

import org.jbanana.core.Convetions.Singleton;

@Singleton
public interface Identity {

	void setId(Object prevalentSystem, Persistent obj);
}