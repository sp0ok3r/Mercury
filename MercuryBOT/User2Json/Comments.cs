/*  
 ▄▀▀▄ ▄▀▄  ▄▀▀█▄▄▄▄  ▄▀▀▄▀▀▀▄  ▄▀▄▄▄▄   ▄▀▀▄ ▄▀▀▄  ▄▀▀▄▀▀▀▄  ▄▀▀▄ ▀▀▄ 
█  █ ▀  █ ▐  ▄▀   ▐ █   █   █ █ █    ▌ █   █    █ █   █   █ █   ▀▄ ▄▀ 
▐  █    █   █▄▄▄▄▄  ▐  █▀▀█▀  ▐ █      ▐  █    █  ▐  █▀▀█▀  ▐     █   
  █    █    █    ▌   ▄▀    █    █        █    █    ▄▀    █        █   
▄▀   ▄▀    ▄▀▄▄▄▄   █     █    ▄▀▄▄▄▄▀    ▀▄▄▄▄▀  █     █       ▄▀    
█    █     █    ▐   ▐     ▐   █     ▐             ▐     ▐       █     
▐    ▐     ▐                  ▐                                 ▐   
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace MercuryBOT.CommentPage
{
    class CommentPage
    {
        /// <summary>
        /// The title of the thread.
        /// </summary>
        public string Title { get; internal set; }

        /// <summary>
        /// The original post made to start the thread.
        /// </summary>
        public Comment OriginalPost { get; internal set; }

        /// <summary>
        /// List of every other comment made on the page, accending to the newest being last.
        /// </summary>
        public List<Comment> Comments { get; internal set; }
        public CommentPage()
        {
            Comments = new List<Comment>();
        }
    }

    class Comment
    {
        /// <summary>
        /// A unique identifier for the comment. If the first post in a topic, then it will match the topic id.
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// The author of the comment
        /// </summary>
        public AuthorDetail Author { get; internal set; }

        /// <summary>
        /// The time the comment was made
        /// </summary>
        public DateTime Posted { get; internal set; }

        /// <summary>
        /// The contents of the comment
        /// </summary>
        public string Content { get; internal set; }

        /// <summary>
        /// A hotlink to the comment
        /// </summary>
        public string Link { get; internal set; }

        public struct AuthorDetail
        {
            /// <summary>
            /// Name of the author
            /// </summary>
            public string Name { get; internal set; }
            
            /// <summary>
            /// Link to the author's profile
            /// </summary>
            public string Profile { get; internal set; }

            /// <summary>
            /// The URL to the author's avatar
            /// </summary>
            public string AvatarURL { get; internal set; }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}