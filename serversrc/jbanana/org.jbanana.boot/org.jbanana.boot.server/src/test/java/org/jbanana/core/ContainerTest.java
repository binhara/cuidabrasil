package org.jbanana.core;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertTrue;
import static org.junit.Assert.fail;

import org.jbanana.core.Convetions.Singleton;
import org.jbanana.exception.InfraRuntimeException;
import org.junit.Test;

public class ContainerTest {
		
	@Test
	public void testReplaceIdentityImpl() throws Throwable {
		Container c = new Container(ContainerTest.class);
		c.clear();
		Identity id = c.my(Identity.class);
		assertTrue(id instanceof IdentityImpl);
		
		class NewIdentityImpl extends IdentityImpl implements Identity{
			@Override
			public void setId(Object prevalentSystem, Persistent obj) {
				//change impl here
			};	
		}
		
		c.registry(new NewIdentityImpl());
		id = c.my(Identity.class);
		assertEquals(id.getClass(), NewIdentityImpl.class);		
	}

	@Test
	public void testContainer() {
		
		Container c = new Container(ContainerTest.class);
		c.clear();
		Bean bean1 = c.my(Bean.class);
		assertTrue(bean1 instanceof BeanImpl);
		
		c.clear();
		Bean bean2 = c.my(Bean.class);
		assertTrue(bean1 != bean2);
		
		c.clear();
		c.registry(new BeanMock());
		Bean bean3 = c.my(Bean.class);
		assertTrue(bean3 instanceof BeanMock);		
		
		c.clear();
		Bean bean4 = c.my(Bean.class);
		assertTrue(bean4 instanceof BeanImpl);		
		
		c.clear();
		try {
			NotValidBean nvb = c.my(NotValidBean.class);
			fail(nvb.toString());
			
		} catch (InfraRuntimeException e) {/*ignore*/}		
		
		c.clear();
		try {
			NotValidBean2 nvb = c.my(NotValidBean2.class);
			fail(nvb.toString());
			
		} catch (InfraRuntimeException e) {/*ignore*/}		
		
		c.clear();
		try {
			c.registry(new NotValidBean2());
			fail();
			
		} catch (InfraRuntimeException e) {/*ignore*/}
	}
}

interface NotValidBean{}
@Singleton class NotValidBean2{}

@Singleton interface Bean{}
class BeanImpl implements Bean{}
class BeanMock implements Bean{}