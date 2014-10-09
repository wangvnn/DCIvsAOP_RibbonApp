using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRibbon.Requirements
{
    /* ANALYSIS inspired by:
     * https://github.com/DCI/scaladci/blob/master/examples/src/test/scala/scaladci/examples/dijkstra/Analysis.scala
     * 
     * RibbonAOP origin
     * Website: ww.aspectroid.com/episodes.html
     * Pdf for design discussion: http://www.aspectroid.com/uploads/9/8/5/4/9854624/ribbons.1.1.pdf
     * 
     * Summary:
     * Ribbon is an APP that allows USER to draw SHAPEs by using their fingers to touch the screen.
     * 
     * USE CASE TEMPLTE from: https://www.cs.duke.edu/courses/cps108/spring04/readings/usecaseslarman.pdf
     * 
     * USE CASE 1: DRAWING [ USER-goal, blackbox style(vs whitebox), fullydressed(vs brief, casual) ]
     * 
     * Summary: USER touches the screen to draw SHAPEs.
     * 
     * Primary Actor: USER
     * 
     * Stakeholders and Interests List:     
     * - USER: wants to draw and see the SHAPEs
     * - APP: wants to record the SHAPE to display 
     * 
     * Scope: DRAWING      
     * Preconditions: APP is opened and ready
     * Postconditions (Success Guarantee): Drew SHAPE is recorded and displayed on screen
     * Trigger: USER wants to draw something
     * 
     * Main Success Scenario ( or basic flow in 2 columns style )
     *                   USER vs APP
     * 1. USER uses his finger to touch the screen to start new DRAWING session.
     *    2. APP acknowledges that USER started to draw
     *    3. APP records the first POINT of the SHAPE
     *    
     * 3. While still touching the screen, USER can move his finger to draw the SHAPE.
     *    4. APP displays the SHAPE on the screen as visual feedback
     *    
     * USER and APP repeat step 3 and 4 until step 5
     *    
     * 5. USER removes his finger to end the DRAWING session.
     *    6. APP shows the final SHAPE to USER as visual feedback
     * 
     * Extensions (Alternative Flows, Deviation):
     * 1.a USER can use more than one finger to draw multiple lines simultaneously 
     *     APPLICATION will display multiple SHAPEs
     * 2.a, 6.a APP may provide other kind of feedbacks (haptic/sound)
     * 3.a [Sub use case TRIMMING] 
     *     While DRAWING the line, the APP detechs the POINTs of the SHAPRE get aged 
     *     and trim them if they are older than a threshold
     * 
     * Special requirements: 
     *  Threshold to trim: 600 ms
     *  
     * Technology and Data Variations List: 
     *  Touch screen is the main input
     *  User can use mouse as alternative input
     *  A simulated touch screen can be plugin
     *  
     * Frequency of Occurence: very often
     *  
     * Open issues: 
     * - Still discussing Bluring, Zeroing use case
     * 
     * USE CASE 2(HABIT): TRIMMING [ sub-use case, blackbox style(vs whitebox), fullydressed(vs brief, casual) ]
     * 
     * APP monitor the SHAPE and trim the aged POINTs of the SHAPE.
     * 
     * Primary Actor: APP
     * 
     * Stakeholders and Interests List:
     * - USER: wants to have visual feedback about which POINTs of the SHAPE was trimmed.
     * - APP: wants to trim the SHAPE to maintain good performance
     * - SHAPE : doesn't want to store the trimmed POINTs.
     * 
     * Scope: DRAWING
     * Preconditions: USER is DRAWING
     * Postconditions: POINT of the SHAPE is trimmed
     * Trigger: POINT of the SHAPE get old
     * 
     * Main Success Scenario ( 2 columns style )
     * APP vs SHAPE
     * 1. APP checks how old each POINT of the SHAPE is.   
     *      If one POINT of the SHAPE is older than a threshold
     *      The APP delete that POINT from the SHAPE as a visual feedback to USER.
     * 
     * Deviation:
     * 1.The APP may provide other kind of feedback: fadeout/bluring
     *  
     * Special requirements:
     * 
     * Frequency of Occurence: continously after USER started to draw
     * 
     * Open issues: Nope
     * 
     * 
     * --------------------------------------------------------------
     * Brainstorming by using
     * Candidate ROLE, RESPONSIBILITY, COLLABORATION (CRC/RRC Cards)
     * 
     * 1st Attempt
     * ---------------------------------------------------------------
     * | ROLE  | RESPONSIBILITY                      | COLLABORATION |
     * ---------------------------------------------------------------
     * | USER  | Proving input for DRAWING session   | APP           |
     * ---------------------------------------------------------------
     * | APP   | Storing SHAPE data                  | USER          |
     * |       | Displaying SHAPE visual             | SHAPE         |
     * |       | TRIMMING SHAPE's data               |               |
     * ---------------------------------------------------------------    
     * | SHAPE | Prepresenting SHAPE                 | POINT         |
     * ---------------------------------------------------------------   
     * | POINT | A Unit component of the SHAPE       |               |
     * ---------------------------------------------------------------
     * 
     * 2nd, 3rd,.... Attemps
     * 
     * --------------------------------------------------------------
     * 
     * Brainstorming by using 
     * DATA, CONTEXT, INTERACTION (DCI)
     * (and info from CRC/RRC)
     * 
     * 1st Attempt
     * ---------------------------------------------------------------
     * | DATA       | POINT, SHAPE                                   |
     * ---------------------------------------------------------------
     * | CONTEXT    | DRAWING, TRIMMING                              | 
     * ---------------------------------------------------------------
     * | INTERACTION|                                                |  
     * | In DRAWING | USER, APP, SHAPE                               |  
     * | In TRIMMING| APP, SHAPE                                     |  
     * --------------------------------------------------------------- 
     * 
     * 2nd Attempt ( based on AOP example )
     * ---------------------------------------------------------------
     * | DATA       |                                                |
     * | In DOMAIN  | POINT, TRACK, TRACKSET                         |
     * | In UI      | RIBBON, RIBBONSET, THICKPAINT                  |
     * | In UI      | SURFACE, TOUCHVIEW                             |
     *  ---------------------------------------------------------------
     * | CONTEXT    | DRAWING, TRIMMING                              | 
     * ---------------------------------------------------------------
     * | INTERACTION|                                                |  
     * | In DRAWING | USER, APP, SHAPE                               |  
     * | In TRIMMING| APP, SHAPE                                     |  
     * --------------------------------------------------------------- 
     * 
     * ---------------------------------------------------------
     * 
     * CODE THIS UP: Working on Domain Model first
     * by porting Point, Track, TrackSet from AOP to DCI
     * (and make sure they are really dump)
     * 
     * Point: 
     *      data: X, Y, 
     *      behavior: 
     *          - answer the distance between 2 points
     *          
     *  Dump check -> YES
     *  
     * Track: 
     *      data: List of Points
     *      behaviors: 
     *          - add new point
     *          - remove a point
     *          - close
     *          - clear
     *          - answer: isClosed, Id, Count
     *  Dump check -> MAYBE YES
     *       
     *  TrackSet: 
     *      data:
     *          - list of Track
     *          - list of openTracks
     *      behaviors:
     *          - start a new track
     *          - add point to a track
     *          - end a track
     *          - clear
     *          - remove a track
     *  Dump check -> MAYBE NOT (depend on progammer mental model)
     *  
     *  The reason: DCI Data deals with storing/manipulating local data
     *  It should not store intermediate states about others (Track)
     *  The info about a Track is open or not is stored in the Track
     *  But the TrackSet contains a list of open track as well
     *  Let's see if we can separate that logic out the TrackSet.
     *  In DCI it looks like it will stay in a Context(Habit)
     *  Let's call it StartNewTrack with 3 Roles: 
     *      theNewTrack, TheTrackSetToAdd, AtPoint 
     *  
     *  NOTE: 
     *     enscape logic/data in TrackSet (OAP)
     *     vs separe data and logic in TrackSet and StartNewTrack context (DCI)
     *     
     *     Track has isClosed property which feels like higher level property
     *     Which should not be in the Track, so that it might be easier
     *     to reuse the Track somewhere else (again depend on programmer mental model).
     *     The isClosed actually means isbeingdrawnByUser
     *     I miss understand isClosed as the property to indicate
     *     a cyclic Track which is very scary when dealing with Track
     * 
     * 3rd Attemp revisit - Simplify the Domain Model
     * 
     * Track: 
     *      data: List of Points
     *      behaviors: 
     *          - add new point
     *          - remove a point
     *          - close (DELETED)
     *          - clear
     *          - answer: isClosed(DELETED), Id, Count
     *  Dump check -> A BETTER YES     
     *  
     *  TrackSet: 
     *      data:
     *          - list of Track
     *          - list of openTracks (DELETED)
     *      behaviors:
     *          - start a new track (DELETED)
     *          - add point to a track (DELETED)
     *          - end a track (DELETED)
     *          - clear 
     *          - remove a track    
     *  Dump check -> SURELY YES :)       
     *  NOTE:   StartNewTrack looks like a factory pattern where we implement
     *  the logic to create an aggregate (TRACKSET)
     *  In reality it may be more complex to create different kinds of POINT
     *  (ie we need to use Brushes not POINT to draw)
     *  So it is good the have a place to capture the algorithm.
     *  
     *  --------------------------------------------------------------------
     * CODE IT UP: The presentation
     * 
     * porting presentation Data from AOP to DCI
     * 
     * Surface:
     * In AOP Surface is the view which has:
     * - TouchView to process input
     * - TrackSet to store SHAPEs data
     * - RibbonSet to store SHAPEs visual
     * They are glued to Surface by AOP Touching, Tracking
     * In traditional OOP this is compostion
     * Composition is good to scale, but it is bad for communication
     * ie componentA need to talks to componentB, they need to go
     * through the owner.
     * For the sake of comparing AOP and DCI
     * I map them back to MVC to use the model from Prokon Example
     * In MVC the View forward input to Controller to handle
     * Then update the Model and then check if Model data was changed
     * And refresh the View
     * In AOP Surface-View have the input, it forward to TouchView
     * TouchView-Controller modify the TrackSet-Model
     * Periodically Movie-Controller will update Surface with RibbonSet 
     * Movie, RibbonSet-SubView is glued with Surface through Rendering AOP
     * 
     * 4nd attemp forming MVC Model (inspired by Prokon)
     * 
     * Surface becomes the master View
     * Handle input in Surface (remove TouchView)
     * Movie becomes RibbonApp(Controller) -> refresh the View
     * RibbonSet(Ribbon)becomes SubView that belong to Surface
     * 
     * NOTE:
     * In tradional OOP, object contains:
     * 1 - Local data + logic to hanel local data
     * 2 - Composite data: list of components
     * 3 - Logic to handle multiple components
     * In the AOP example: 1 stays, 2 and 3 was taken out of object
     * That makes it cleaner. 2 and 3 will be injected throught AOP
     * 
     * In DCI: Logic that is related to Use Case (usually crossing multiple object logic)
     * is taken out of object and it will be injected through Role Method
     * Those logic also will be gathered and put in a central Context (mapped to Usecase)
     * 
     * There is something in common here between two methods.
     * AOP improves the current situation of tradional OOP.
     * Algo still is need to be across multiple objects -> hard to read
     * DCI aims for gathering Use Case related behaviours into central place
     * so that we can read and reason them with requirement (Use Case, User Metal Model)
     *
     * CODE IT UP:
     * 
     * USE CASE 1: updated (inspired by HAXE SnakeDCI
     * https://github.com/ciscoheat/SnakeDCI/blob/master/Use%20cases.md)
     * 1. User starts drawing [StartDrawing]
     * 2. User keeps drawing [KeepDrawing]
     * 3. User stops drawing [StopDrawing]
     * 
     * USE CASE 1.1 StartDrawing
     * USER vs APP
     * 1.USER starts drawing the SHAPE
     *    2. App starts a new SHAPE [StartNewTrack]
     *    3. App tracks which SHAPE is being drew
     * 
     * USE CASE 1.2 KeepDrawing
     * 1. USER keeps drawing the SHAPE 
     *    2. App updates the SHAPE [UpdatingTrack]
     * 
     * USE CASE 1.3 StopDrawing
     * 1. USER stops drawing the SHAPE 
     *    2. App stops the SHAPE [StopTrack]
     * 
     * [StartDrawing] (Presentation/Applcation Context)
     * Roles: NewShape,ShapeTracker (App/System), PointToStart
     * Interaction: 
     * Start: 
     * 1. Run [StartNewTrack] Context (Domain Data Context)
     * 2. App/System remembers which SHAPE is being drawn
     * 
     * [KeepDrawing] Context (Presentation/Application Context)
     * Roles: NewPointToAdd, TouchScreen, BeingDrawnShapeId, BeingDrawShape
     * Draw:
     * 1. BeingDrawShape = App/System.FindBeingDrawnShape(BeingDrawnShapeId)
     * 2. Run [UpdateTrack] with BeingDrawnShape, NewPointToAdd (Domain Data Context)

     * [StopDrawing] (Presentation/Applcation Context)
     * Roles: LastPointToAdd, ShapeTracker (App/System), TouchRecorder, BeingDrawnShape
 
     * * Stop: 
     * 1. ShapeTracker find being drawn SHAPE [FindBeingDrawnShape]
     * 1. Run [StopTrack] Context (Domain Data Context) to stop that SHAPE
     * 2. App/System forget this SHAPE is being drawn
     * 
     * One thing is interesting here:
     * Usually we need to construct all the roles before executing the context
     * But I include the FindShape step in the context so in this case we don't know
     * the role SHAPE until the SHAPE_TRACKER find it.
     * And Marvin doesn't allow to create the Role while executing the Context :)
     * 
     * Conclusion:
     * 
     * AOP: Well done tradional OOP with advance techique AOP to
     * - inject components into composite
     * + ie RibbonSet (View) in to Surface(master view)
     * + ie TouchPad (input handler) into Surface(master view)
     * - inject behaviours that needs many components to interact with each others
     * + ie onTouchDown: TouchPad tell the Tracks in Surface to start.
     * 
     * The power of AOP: can inject fields, methods and can control when to inject.
     * As a result:
     * Classes are small, simple, still smart, and highly reusable.
     * 
     * Using AOP to encapsulate the glueing logic and interaction logic.
     * However flow/algorithm is still distributed to many object
     * Hence master flow still get lost as usual.
     * 
     * DCI: capture the master flow(algo) which is very close to use case.
     * In the presentation/application layer, contexts look like 
     * The EBP Use Case ( elementary business processes )
     *      "EBP is a term from the business process engineering field4, defined as: 
     *      A task performed by one person in one place at one time, in
     *      response to a business event, which adds measurable business
     *      value and leaves the data in a consistent state. e.g., Approve
     *      Credit or Price Order [original source lost]."
     * And in the domain, data contexts looks like use case as low level
     * called a subfunction goal—subgoals that support a user goal.
     * "It is not illegal to write use cases for subfunction goals, 
     * but it is not always helpful as it adds complexity to a use case model"
     * https://www.cs.duke.edu/courses/cps108/spring04/readings/usecaseslarman.pdf
     * 
     * DCI is orthogonal to AOP, so we can still apply the above AOP technique
     * We can still use AOP to glue components into composite
     * ie: RibbonSet into Surface, TrackSet into Controller/Surface...
     * We can also inject behavior of Surface to handle input event
     * and trigger different Algo/Context to run the use case.
     * If we can get the good of both then that will be very powerful technique.
     * Too bad, I don't know any good languge can support both DCI and AOP well.
     * Marvin is good for DCI but not AOP, Java is good for AOP but not DCI.
     * So the next programming languge that can support both will win my heart :D
     * 
     * There are 2 analogies we can use for each:
     * 
     * AOP is like improved version of lego brick.
     * For example current lego brick, you cannot plug the 3 into the 4 at centralize it.
     * The 3 have to be either on the left or right due to the predefined hook
     * Imagine if we can have the hook that can be adjusted or even no hook at all
     * Then we can easily plug the 3 into the 4 center with some magic glue :)
     * That will be even more flexible than ever.
     * 
     * DCI can be view as this famouse presentation in TED:
     * http://www.ted.com/talks/quyen_nguyen_color_coded_surgery?language=en
     * "At TEDMED Quyen Nguyen demonstrates
     * how a molecular marker can make tumors light up in neon green, 
     * showing surgeons exactly where to cut"
     * 
     * Imaging if we can have IDE that can set color for all the code of 
     * a specific use case so that we can trace/read it easier like the example above.
     * It is event better if we can gather all of that code together in one placce to 
     * read for reasoning, debating...
     * That would be awesome. And that is DCI.
     * 
     * So DCI is the idea that is worth to spread. I want to see it in TED soon :)
    */
}
