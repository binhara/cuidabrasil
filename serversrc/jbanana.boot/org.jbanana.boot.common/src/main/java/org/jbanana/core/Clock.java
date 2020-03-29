package org.jbanana.core;

import java.util.Date;
import org.jbanana.core.Convetions.Singleton;

@Singleton
public interface Clock {

	Date now(); 
}
