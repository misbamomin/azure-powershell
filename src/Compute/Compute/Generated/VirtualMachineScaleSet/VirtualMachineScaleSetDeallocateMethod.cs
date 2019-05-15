//
// Copyright (c) Microsoft and contributors.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//
// See the License for the specific language governing permissions and
// limitations under the License.
//

// Warning: This code was generated by a tool.
//
// Changes to this file may cause incorrect behavior and will be lost if the
// code is regenerated.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Commands.Compute.Automation.Models;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.Compute;
using Microsoft.Azure.Management.Compute.Models;
using Microsoft.WindowsAzure.Commands.Utilities.Common;

namespace Microsoft.Azure.Commands.Compute.Automation
{
    [Cmdlet(VerbsLifecycle.Stop, ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "Vmss", DefaultParameterSetName = "DefaultParameter", SupportsShouldProcess = true)]
    [OutputType(typeof(PSOperationStatusResponse))]
    public partial class StopAzureRmVmss : ComputeAutomationBaseCmdlet
    {
        public override void ExecuteCmdlet()
        {
            base.ExecuteCmdlet();
            ExecuteClientAction(() =>
            {
                if (ShouldProcess(this.VMScaleSetName, VerbsLifecycle.Stop)
                    && (this.Force.IsPresent ||
                        this.ShouldContinue(Properties.Resources.ResourceStoppingConfirmation,
                                            "Stop-AzVmss operation")))
                {
                    string resourceGroupName = this.ResourceGroupName;
                    string vmScaleSetName = this.VMScaleSetName;
                    System.Collections.Generic.IList<string> instanceIds = this.InstanceId;

                    Rest.Azure.AzureOperationResponse result = null;
                    if (this.ParameterSetName.Equals("FriendMethod"))
                    {
                        bool? skipShutdown = this.SkipShutdown.IsPresent ? (bool?) true : null;
                        result = VirtualMachineScaleSetsClient.PowerOffWithHttpMessagesAsync(resourceGroupName, vmScaleSetName, skipShutdown, instanceIds).GetAwaiter().GetResult();
                    }
                    else
                    {
                        result = VirtualMachineScaleSetsClient.DeallocateWithHttpMessagesAsync(resourceGroupName, vmScaleSetName, instanceIds).GetAwaiter().GetResult();
                    }

                    PSOperationStatusResponse output = new PSOperationStatusResponse
                    {
                        StartTime = this.StartTime,
                        EndTime = DateTime.Now
                    };

                    if (result != null && result.Request != null && result.Request.RequestUri != null)
                    {
                        output.Name = GetOperationIdFromUrlString(result.Request.RequestUri.ToString());
                    }

                    WriteObject(output);
                }
            });
        }

        [Parameter(
            ParameterSetName = "DefaultParameter",
            Position = 0,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true)]
        [Parameter(
            ParameterSetName = "FriendMethod",
            Position = 0,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true)]
        [ResourceGroupCompleter]
        public string ResourceGroupName { get; set; }

        [Parameter(
            ParameterSetName = "DefaultParameter",
            Position = 1,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true)]
        [Parameter(
            ParameterSetName = "FriendMethod",
            Position = 1,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true)]
        [ResourceNameCompleter("Microsoft.Compute/virtualMachineScaleSets", "ResourceGroupName")]
        [Alias("Name")]
        public string VMScaleSetName { get; set; }

        [Parameter(
            ParameterSetName = "DefaultParameter",
            Position = 2,
            ValueFromPipelineByPropertyName = true)]
        [Parameter(
            ParameterSetName = "FriendMethod",
            Position = 2,
            ValueFromPipelineByPropertyName = true)]
        public string [] InstanceId { get; set; }

        [Parameter(
            ParameterSetName = "DefaultParameter",
            Mandatory = false)]
        [Parameter(
            ParameterSetName = "FriendMethod",
            Mandatory = false)]
        public SwitchParameter Force { get; set; }

        [Parameter(
            ParameterSetName = "FriendMethod",
            Mandatory = true)]
        public SwitchParameter StayProvisioned { get; set; }

        [Parameter(
            ParameterSetName = "FriendMethod")]
        public SwitchParameter SkipShutdown { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Run cmdlet in the background")]
        public SwitchParameter AsJob { get; set; }
    }
}
