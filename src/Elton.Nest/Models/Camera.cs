#region License

//   Copyright 2018 Elton FAN (eltonfan@live.cn, http://elton.io)
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License. 

#endregion

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Elton.Nest.Models
{
    /// <summary>
    /// Camera represents a single Nest Camera device. It contains all information associated with a
    /// Camera device.
    /// </summary>
    [DataContract]
    public class Camera : Device, IEquatable<Camera>, IValidatableObject
    {
        public const string KEY_IS_STREAMING = "is_streaming";

        /// <summary>
        /// Initializes a new instance of the <see cref="Device" /> class.
        /// </summary>
        [JsonConstructor]
        protected Camera() { }

        public Camera(bool IsStreaming = default)
        {
            this.IsStreaming = IsStreaming;
        }
        /// <summary>
        /// Returns whether the camera is currently streaming.
        /// </summary>
        /// <value>true if the camera is streaming, false otherwise.</value>
        [DataMember(Name = "is_streaming")]
        public bool IsStreaming { get; set; }

        /// <summary>
        /// Returns whether audio input is enabled on the camera.
        /// </summary>
        /// <value>true if the camera has audio input enabled, false otherwise.</value>
        [DataMember(Name = "is_audio_input_enabled")]
        public bool IsAudioInputEnabled { get; set; }

        /// <summary>
        /// Returns a timestamp of the last time that the camera changed its isOnline state.
        ///
        /// @return A timestamp of the last time that the camera changed its isOnline state.
        /// </summary>
        [DataMember(Name = "last_is_online_change")]
        public string LastIsOnlineChange { get; set; }

        /// <summary>
        /// Returns whether video history is enabled on the camera.
        ///
        /// @return true if the camera has video history enabled, false otherwise.
        /// </summary>
        [DataMember(Name = "is_video_history_enabled")]
        public bool IsVideoHistoryEnabled { get; set; }

        /// <summary>
        /// Returns a web URL (deep link) to the live camera feed at home.nest.com.
        ///
        /// @return A web URL (deep link) to the live camera feed at home.nest.com.
        /// </summary>
        [DataMember(Name = "web_url")]
        public string WebUrl { get; set; }

        /// <summary>
        /// Returns an app URL (deep link) to the live camera feed in the Nest app.
        ///
        /// @return An app URL (deep link) to the live camera feed in the Nest app.
        /// </summary>
        [DataMember(Name = "app_url")]
        public string AppUrl { get; set; }

        /// <summary>
        /// Returns a LastEvent object containing information about the last event.
        ///
        /// @return A LastEvent object representing the data captured in the last event.
        /// </summary>
        [DataMember(Name = "last_event")]
        public LastEvent LastEventValue { get; set; }

        /// <summary>
        /// Returns whether public sharing is enabled on the camera.
        ///
        /// @return True if public sharing is enabled on the camera.
        /// </summary>
        [DataMember(Name = "is_public_share_enabled")]
        public bool IsPublicShareEnabled { get; set; }

        /// <summary>
        /// Returns a list of HashMaps that contain an activity zone.
        ///
        /// @return ArrayList of HashMaps that contain an activity zone.
        /// </summary>
        [DataMember(Name = "activity_zones")]
        public List<ActivityZone> ActivityZones { get; set; } = new List<ActivityZone>();

        /// <summary>
        /// Returns a URL of the shared public stream.
        ///
        /// @return A string URL of the shared public stream.
        /// </summary>
        [DataMember(Name = "public_share_url")]
        public string PublicShareUrl { get; set; }

        /// <summary>
        /// Returns a URL to this camera's snapshots.
        ///
        /// @return A string URL to this camera's snapshots.
        /// </summary>
        [DataMember(Name = "snapshot_url")]
        public string SnapshotUrl { get; set; }

        public override string ToString()
        {
            return Utils.ToString(this);
        }

        public override int GetHashCode()
        {
            return Utils.GetHashCode(this);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Camera);
        }

        public bool Equals(Camera other)
        {
            return Utils.AreEqual(this, other);
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }

        /// <summary>
        /// LastEvent contains information about the last event that triggered a notification. In order
        /// to capture last event data, the Nest Cam must have a Nest Aware with Video History
        /// subscription.
        /// </summary>
        [DataContract]
        public class LastEvent : IEquatable<LastEvent>, IValidatableObject
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="LastEvent" /> class.
            /// </summary>
            [JsonConstructor]
            protected LastEvent()
            {
                ActivityZoneIds = new List<string>();
            }

            /// <summary>
            /// Returns whether sound was detected in the last event.
            ///
            /// @return true if sound was detected, false if sound was not detected.
            /// </summary>
            [DataMember(Name = "has_sound")]
            public bool HasSound { get; set; }

            /// <summary>
            /// Returns whether motion was detected in the last event.
            ///
            /// @return true if motion was detected, false if motion was not detected.
            /// </summary>
            [DataMember(Name = "has_motion")]
            public bool HasMotion { get; set; }

            /// <summary>
            /// Returns whether person was detected in the last event.
            ///
            /// @return true if person was detected, false if motion was not detected.
            /// </summary>
            [DataMember(Name = "has_person")]
            public bool HasPerson { get; set; }

            /// <summary>
            /// Returns the time of the start of the event.
            ///
            /// @return Event start time, in ISO 8601 format.
            /// </summary>
            [DataMember(Name = "start_time")]
            public string StartTime { get; set; }

            /// <summary>
            /// Returns the time of the end of the event.
            ///
            /// @return Event end time, in ISO 8601 format.
            /// </summary>
            [DataMember(Name = "end_time")]
            public string EndTime { get; set; }

            /// <summary>
            /// Returns the time that the URLs expire, in ISO 8601 format.
            ///
            /// @return the time that the URLs expire, in ISO 8601 format.
            /// </summary>
            [DataMember(Name = "urls_expire_time")]
            public string UrlsExpireTime { get; set; }

            /// <summary>
            /// Returns a web URL (deep link) to the last sound or motion event at home.nest.com. Used to
            /// display the last recorded event, and requires user to be signed in to the account.
            ///
            /// @return A web URL (deep link) to the last sound or motion event.
            /// </summary>
            [DataMember(Name = "web_url")]
            public string WebUrl { get; set; }

            /// <summary>
            /// Returns a Nest app URL (deep link) to the last sound or motion event. Used to display the
            /// last recorded event, and requires user to be signed in to the account.
            ///
            /// @return An app URL (deep link) to the last sound or motion event.
            /// </summary>
            [DataMember(Name = "app_url")]
            public string AppUrl { get; set; }

            /// <summary>
            /// Returns a URL (link) to the image file captured for a sound or motion event.
            /// </summary>
            /// <value>
            /// A URL (link) to the image file captured for a sound or motion event.
            /// </value>
            [DataMember(Name = "image_url")]
            public string ImageUrl { get; set; }

            /// <summary>
            /// Returns a URL (link) to the gif file captured for a sound or motion event.
            /// </summary>
            /// <value>
            /// A URL (link) to the gif file captured for a sound or motion event.
            /// </value>
            [DataMember(Name = "animated_image_url")]
            public string AnimatedImageUrl { get; set; }

            /// <summary>
            /// Returns a list of zone ids that detected motion during the last event.
            /// </summary>
            /// <value>
            /// An ArrayList of zone ids that detected motion during the last event.
            /// </value>
            [DataMember(Name = "activity_zone_ids")]
            public List<string> ActivityZoneIds { get; set; }

            public override string ToString()
            {
                return Utils.ToString(this);
            }

            public override int GetHashCode()
            {
                return Utils.GetHashCode(this);
            }

            public override bool Equals(object obj)
            {
                return this.Equals(obj as LastEvent);
            }

            public bool Equals(LastEvent other)
            {
                return Utils.AreEqual(this, other);
            }

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                yield break;
            }
        }

        [DataContract]
        public class ActivityZone : IEquatable<ActivityZone>, IValidatableObject
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ActivityZone" /> class.
            /// </summary>
            [JsonConstructor]
            protected ActivityZone() { }

            [DataMember(Name = "name")]
            public string Name { get; set; } = "";

            [DataMember(Name = "id")]
            public string Id { get; set; } = "";

            public override string ToString()
            {
                return Utils.ToString(this);
            }

            public override int GetHashCode()
            {
                return Utils.GetHashCode(this);
            }

            public override bool Equals(object obj)
            {
                return this.Equals(obj as ActivityZone);
            }

            public bool Equals(ActivityZone other)
            {
                return Utils.AreEqual(this, other);
            }

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                yield break;
            }
        }
    }
}