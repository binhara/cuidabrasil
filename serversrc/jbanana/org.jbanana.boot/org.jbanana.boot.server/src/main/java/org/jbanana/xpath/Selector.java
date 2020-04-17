package org.jbanana.xpath;

public interface Selector<T> {

	boolean match(T candidate);
}