DCIvsAOP_RibbonApp
==================

A side by side comparison of two technique to program an OOP application.

1) The amazing aspectroid: http://www.aspectroid.com/episodes.html using AOP as an advanced technique to achieve:
(The design discussion: http://www.aspectroid.com/uploads/9/8/5/4/9854624/ribbons.1.1.pdf)

- Very small, well defined classes but still smart enough.
- Highly reuseable for any layer: View, Input Handler, Rendering, Domain Model...
- Very easy to add new behaviours without changing the code.
This is the state of the art how to do OOP right. 
And Mr Carlo Pescio is an expert in this field.

2) The DCI paradigm:
http://en.wikipedia.org/wiki/Data,_context_and_interaction

The starting post in DCI google group:
https://groups.google.com/forum/#!topic/object-composition/AXCvxWpemi4

Conclusion:

Short verions:

RibbonApp built by traditional OOP done right with the magic of AOP is very with the above properties.
Which I believe it is state of the art of OOP done by OOP expert.

RibbonApp built by DCI helps to capture algo/flow/usecase related code into one place.
And make it more readable, understandable hence changable.

End of the date the both techniques are orthogonal so they can be use together without any problem.

Very long version:
https://github.com/wangvnn/DCIvsAOP_RibbonApp/blob/master/RibbonAOP/MyRibbonDCI/00%20Requirements/RibbonAppRequirements.cs

AOP Ribbon APP: 
http://www.lego.com/en-us/fusion/videos/how-did-he-do-that-unbelievable-ipad-magic-using-lego-bricks-3284783469c1445aa1ba9fb43029fb0d

vs

DCI Ribbon App:
http://www.ted.com/talks/quyen_nguyen_color_coded_surgery?language=en

Tools: 
Marvin: http://fulloo.info/Examples/Marvin/Introduction/
Linfu Proxu for AOP: http://www.codeproject.com/Articles/20884/Introducing-the-LinFu-Framework-Part-I-LinFu-Dynam
