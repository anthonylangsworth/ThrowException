ThrowException
==============

A simple experiment in using System.Linq.Expression to automatically include the argument name in commonly thrown exceptions, such as ArgumentNullException and ArgumentException.

This ended up similar to answers to questions on stack overflow such as http://stackoverflow.com/questions/12043875/argumentnullexception-how-to-simplify.

Examples
--------

Instead of:

	if(argument1 == null) throw new ArgumentNullException("argument1");
	if(argument2 == null) throw new ArgumentNullException("argument2");

at the top of a method to check for null arguments and throw the
appropriate <see cref="ArgumentNullException"/>s, use:

	ThrowException.ThrowArgumentNullException.IfNull(
		() => argument1, () => argument2);

The ArgumentNullException thrown contains the correct argument name. This synatx is
much cleaner and ensures argument renames are reflexted in the argument name used
in the exception.

There is a similar class for ArgumentException:

	ThrowException.ThrowArgmentException.If(() => argument, 
		argument => argument > 0, "Not valid exception message");


License
-------

See LICENSE.txt (MIT).
