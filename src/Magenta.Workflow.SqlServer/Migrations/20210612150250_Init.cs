using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Magenta.Workflow.SqlServer.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlowTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    EntityType = table.Column<string>(nullable: true),
                    EntityPayloadType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlowInstances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    TypeId = table.Column<Guid>(nullable: false),
                    InitializerId = table.Column<string>(nullable: true),
                    AccessPhrase = table.Column<string>(nullable: true),
                    Payload = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowInstances_FlowTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "FlowTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlowStates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Tag = table.Column<string>(nullable: true),
                    StateType = table.Column<byte>(nullable: false),
                    TypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowStates_FlowTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "FlowTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlowIdentities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    StateId = table.Column<Guid>(nullable: false),
                    IdentityType = table.Column<byte>(nullable: false),
                    IdentityId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowIdentities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowIdentities_FlowStates_StateId",
                        column: x => x.StateId,
                        principalTable: "FlowStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlowTransitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TransitionType = table.Column<int>(nullable: false),
                    IsAutomatic = table.Column<bool>(nullable: false),
                    SourceId = table.Column<Guid>(nullable: true),
                    DestinationId = table.Column<Guid>(nullable: false),
                    TypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowTransitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowTransitions_FlowStates_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "FlowStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowTransitions_FlowStates_SourceId",
                        column: x => x.SourceId,
                        principalTable: "FlowStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowTransitions_FlowTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "FlowTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlowSteps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    InstanceId = table.Column<Guid>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    Payload = table.Column<string>(nullable: true),
                    TransitionId = table.Column<Guid>(nullable: false),
                    IdentityId = table.Column<string>(nullable: true),
                    IsCurrent = table.Column<bool>(nullable: false),
                    CurrentStateName = table.Column<string>(nullable: true),
                    CurrentStateTitle = table.Column<string>(nullable: true),
                    CurrentStateType = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowSteps_FlowInstances_InstanceId",
                        column: x => x.InstanceId,
                        principalTable: "FlowInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlowSteps_FlowTransitions_TransitionId",
                        column: x => x.TransitionId,
                        principalTable: "FlowTransitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlowTransitionReasons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TransitionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowTransitionReasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowTransitionReasons_FlowTransitions_TransitionId",
                        column: x => x.TransitionId,
                        principalTable: "FlowTransitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlowIdentities_StateId",
                table: "FlowIdentities",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowInstances_TypeId",
                table: "FlowInstances",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowStates_TypeId",
                table: "FlowStates",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowSteps_InstanceId",
                table: "FlowSteps",
                column: "InstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowSteps_TransitionId",
                table: "FlowSteps",
                column: "TransitionId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowTransitionReasons_TransitionId",
                table: "FlowTransitionReasons",
                column: "TransitionId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowTransitions_DestinationId",
                table: "FlowTransitions",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowTransitions_SourceId",
                table: "FlowTransitions",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowTransitions_TypeId",
                table: "FlowTransitions",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlowIdentities");

            migrationBuilder.DropTable(
                name: "FlowSteps");

            migrationBuilder.DropTable(
                name: "FlowTransitionReasons");

            migrationBuilder.DropTable(
                name: "FlowInstances");

            migrationBuilder.DropTable(
                name: "FlowTransitions");

            migrationBuilder.DropTable(
                name: "FlowStates");

            migrationBuilder.DropTable(
                name: "FlowTypes");
        }
    }
}
