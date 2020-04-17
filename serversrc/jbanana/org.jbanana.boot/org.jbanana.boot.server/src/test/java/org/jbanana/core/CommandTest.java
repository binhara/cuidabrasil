package org.jbanana.core;

import static org.junit.Assert.assertEquals;
import java.io.Serializable;
import java.lang.reflect.Method;
import java.util.Date;
import org.junit.Test;
import lombok.AllArgsConstructor;

public class CommandTest {

	@Test
	public void testExecuteAndQuery() throws Throwable {
		Class<?> controller = MockEntryPoint.class;
		Method method1 = controller.getDeclaredMethod("restoreAll", new Class[0]);
		Method method2 = controller.getDeclaredMethod("restoreByName", new Class[]{String.class});
		
		Command cmd = new Command(new Object[0], controller, null, method1, null, null, null, null);
		Object result = cmd.executeAndQuery(null, null);
		assertEquals("empty", result);
		
		cmd = new Command(new Object[]{"sandro"}, controller, null, method2, null, null, null, null);
		result = cmd.executeAndQuery(null, null);
		assertEquals("sandro", result);
	}

}

@AllArgsConstructor
class MockEntryPoint {
	
	protected Serializable ps;
	protected Date now;
	protected String nextId;
	
	public MockEntryPoint(Serializable ps, Date now) { }
	public Object restoreAll(){return "empty";}
	public Object restoreByName(String name){ return name;}
}
