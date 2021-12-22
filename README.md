This is a simple implementation concept for separating superadmin/administration bounded context and implementing it in a separate subsystem.
The solution is not sophisticated as classes depend on concrete types and not abstractions - this is only a demonstration.

Users operating in the main context create group events and other content* and make them visible on the platform. After any such piece of content is created, updated etc. the superadmin is notified about it and the textual content (which can be misused) is sent to them.
If the superadmin removes a piece of content, the notification about this will be sent back to the main context, where this information will be stored as well. This is to avoid having to ask the superadmin whether the resource is available in every use case, which would lead to additional network calls. Scaling would also be an issue.
Basically, there is a bidirectional asynchronous communication between contexts. On the boundaries, in the ACL layers, services for publishing and handling events are implemented.

_*content is an entity created and made public by a user - the textual content of it is what the superadmin is interested in._

Subsystems communicate via `NServiceBus` events. Some examples of messages are:
1. A group event is created and superadmin is being notified about a new piece of content that was published on the platform.


2. A group event was removed by the user who created it and superadmin is being notified to remove appropriate records from their database, as there is no need for tracking or moderating this piece of content anymore.

3. A piece of content (e.g. group event) was (marked as) removed by the superadmin and the main context is being notified about this. In the main database, this event will also be marked as removed by the superadmin (this property can be configured as a shadow property). EF Core filter can be established so that entities removed by the superadmin are never returned from the database.
