package org.jbanana.core;

import java.lang.reflect.Field;
import java.util.Map;

import org.jbanana.core.Convetions.Singleton;
import org.jbanana.exception.InfraRuntimeException;

@Singleton
public class IdentityImpl implements Identity{

	@Override
	public void setId(Object prevalentSystem, Persistent obj) {
		
		RootAndSequences ras =null;
		if(prevalentSystem instanceof RootAndSequences) {
			ras = (RootAndSequences) prevalentSystem;
			prevalentSystem = ((RootAndSequences)prevalentSystem).getPrevalentSystem();
		}
		
		try {
			Class<?> clazz = obj.getClass();
			Map<Class<?>, Long> seqs = ras.getSequences();
			if(!seqs.containsKey(clazz))
				seqs.put(clazz, 0l);
			
			long result = seqs.get(clazz).longValue()+1;
			seqs.put(clazz, result);
			String id = ""+result;
			
			Field field = obj.getClass().getDeclaredField("id");
			field.setAccessible(true);
			field.set(obj, id);
			
		} catch (Throwable ex) {
			throw new InfraRuntimeException(ex);
		}
	}
}
