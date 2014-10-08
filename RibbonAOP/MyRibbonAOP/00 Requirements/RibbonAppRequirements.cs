using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRibbon.Requirements
{
    /* Website: ww.aspectroid.com/episodes.html
     * Pdf: http://www.aspectroid.com/uploads/9/8/5/4/9854624/ribbons.1.1.pdf
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
     *    3. APP records/displays the first POINT of the SHAPE
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
     * 2.a/6.a APP may provide other kind of feedbacks (haptic/sound)
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
     * USE CASE 2: TRIMMING [ sub-use case, blackbox style(vs whitebox), fullydressed(vs brief, casual) ]
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
     * Frequency of Occurence: very often
     * 
     * Open issues:
    */
}
