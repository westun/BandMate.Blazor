using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BandMate.Domain.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    B2CObjectIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressID);
                });

            migrationBuilder.CreateTable(
                name: "Band",
                columns: table => new
                {
                    BandID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Band", x => x.BandID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "AccountCredential",
                columns: table => new
                {
                    AccountCredentialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCredential", x => x.AccountCredentialID);
                    table.ForeignKey(
                        name: "FK_AccountCredential_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PasswordResetRequests",
                columns: table => new
                {
                    PasswordResetRequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Used = table.Column<bool>(type: "bit", nullable: false),
                    Revoked = table.Column<bool>(type: "bit", nullable: false),
                    Expires = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResetRequests", x => x.PasswordResetRequestID);
                    table.ForeignKey(
                        name: "FK_PasswordResetRequests_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Venue",
                columns: table => new
                {
                    VenueID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venue", x => x.VenueID);
                    table.ForeignKey(
                        name: "FK_Venue_Address_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Address",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BandAccount",
                columns: table => new
                {
                    BandID = table.Column<int>(type: "int", nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    Default = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BandAccount", x => new { x.BandID, x.AccountID });
                    table.ForeignKey(
                        name: "FK_BandAccount_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BandAccount_Band_BandID",
                        column: x => x.BandID,
                        principalTable: "Band",
                        principalColumn: "BandID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BandName",
                columns: table => new
                {
                    BandNameID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BandID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BandName", x => x.BandNameID);
                    table.ForeignKey(
                        name: "FK_BandName_Band_BandID",
                        column: x => x.BandID,
                        principalTable: "Band",
                        principalColumn: "BandID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Election",
                columns: table => new
                {
                    ElectionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BandID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Election", x => x.ElectionID);
                    table.ForeignKey(
                        name: "FK_Election_Band_BandID",
                        column: x => x.BandID,
                        principalTable: "Band",
                        principalColumn: "BandID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SetList",
                columns: table => new
                {
                    SetListID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    BandID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "getutcdate()"),
                    Modified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetList", x => x.SetListID);
                    table.ForeignKey(
                        name: "FK_SetList_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SetList_Band_BandID",
                        column: x => x.BandID,
                        principalTable: "Band",
                        principalColumn: "BandID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SongListType",
                columns: table => new
                {
                    SongListTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BandID = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongListType", x => x.SongListTypeID);
                    table.ForeignKey(
                        name: "FK_SongListType_Band_BandID",
                        column: x => x.BandID,
                        principalTable: "Band",
                        principalColumn: "BandID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountRole",
                columns: table => new
                {
                    AccountsAccountID = table.Column<int>(type: "int", nullable: false),
                    RolesRoleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRole", x => new { x.AccountsAccountID, x.RolesRoleID });
                    table.ForeignKey(
                        name: "FK_AccountRole_Account_AccountsAccountID",
                        column: x => x.AccountsAccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountRole_Role_RolesRoleID",
                        column: x => x.RolesRoleID,
                        principalTable: "Role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gig",
                columns: table => new
                {
                    GigID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenueID = table.Column<int>(type: "int", nullable: false),
                    BandID = table.Column<int>(type: "int", nullable: false),
                    SetListID = table.Column<int>(type: "int", nullable: true),
                    StartTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gig", x => x.GigID);
                    table.ForeignKey(
                        name: "FK_Gig_Band_BandID",
                        column: x => x.BandID,
                        principalTable: "Band",
                        principalColumn: "BandID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gig_SetList_SetListID",
                        column: x => x.SetListID,
                        principalTable: "SetList",
                        principalColumn: "SetListID");
                    table.ForeignKey(
                        name: "FK_Gig_Venue_VenueID",
                        column: x => x.VenueID,
                        principalTable: "Venue",
                        principalColumn: "VenueID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Song",
                columns: table => new
                {
                    SongID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BandID = table.Column<int>(type: "int", nullable: false),
                    CreatedByAccountID = table.Column<int>(type: "int", nullable: true),
                    SongListTypeID = table.Column<int>(type: "int", nullable: true),
                    Artist = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bpm = table.Column<int>(type: "int", nullable: true),
                    KeySignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mp3FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YoutubeURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TabFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LyricFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SheetMusicFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SheetMusicURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LyricURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Song", x => x.SongID);
                    table.ForeignKey(
                        name: "FK_Song_Account_CreatedByAccountID",
                        column: x => x.CreatedByAccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK_Song_Band_BandID",
                        column: x => x.BandID,
                        principalTable: "Band",
                        principalColumn: "BandID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Song_SongListType_SongListTypeID",
                        column: x => x.SongListTypeID,
                        principalTable: "SongListType",
                        principalColumn: "SongListTypeID");
                });

            migrationBuilder.CreateTable(
                name: "ElectionSong",
                columns: table => new
                {
                    ElectionID = table.Column<int>(type: "int", nullable: false),
                    SongID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectionSong", x => new { x.ElectionID, x.SongID });
                    table.ForeignKey(
                        name: "FK_ElectionSong_Election_ElectionID",
                        column: x => x.ElectionID,
                        principalTable: "Election",
                        principalColumn: "ElectionID");
                    table.ForeignKey(
                        name: "FK_ElectionSong_Song_SongID",
                        column: x => x.SongID,
                        principalTable: "Song",
                        principalColumn: "SongID");
                });

            migrationBuilder.CreateTable(
                name: "ElectionVote",
                columns: table => new
                {
                    ElectionID = table.Column<int>(type: "int", nullable: false),
                    SongID = table.Column<int>(type: "int", nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    Remove = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectionVote", x => new { x.ElectionID, x.SongID, x.AccountID });
                    table.ForeignKey(
                        name: "FK_ElectionVote_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK_ElectionVote_Election_ElectionID",
                        column: x => x.ElectionID,
                        principalTable: "Election",
                        principalColumn: "ElectionID");
                    table.ForeignKey(
                        name: "FK_ElectionVote_Song_SongID",
                        column: x => x.SongID,
                        principalTable: "Song",
                        principalColumn: "SongID");
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    RatingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SongID = table.Column<int>(type: "int", nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    Rated = table.Column<double>(type: "float", nullable: false),
                    Priority = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.RatingID);
                    table.ForeignKey(
                        name: "FK_Rating_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rating_Song_SongID",
                        column: x => x.SongID,
                        principalTable: "Song",
                        principalColumn: "SongID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SetListItem",
                columns: table => new
                {
                    SetListItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SongID = table.Column<int>(type: "int", nullable: false),
                    SetListID = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetListItem", x => x.SetListItemID);
                    table.ForeignKey(
                        name: "FK_SetListItem_SetList_SetListID",
                        column: x => x.SetListID,
                        principalTable: "SetList",
                        principalColumn: "SetListID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SetListItem_Song_SongID",
                        column: x => x.SongID,
                        principalTable: "Song",
                        principalColumn: "SongID");
                });

            migrationBuilder.CreateTable(
                name: "SongAccount",
                columns: table => new
                {
                    SongID = table.Column<int>(type: "int", nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongAccount", x => new { x.SongID, x.AccountID });
                    table.ForeignKey(
                        name: "FK_SongAccount_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SongAccount_Song_SongID",
                        column: x => x.SongID,
                        principalTable: "Song",
                        principalColumn: "SongID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountCredential_AccountID",
                table: "AccountCredential",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRole_RolesRoleID",
                table: "AccountRole",
                column: "RolesRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_BandAccount_AccountID",
                table: "BandAccount",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_BandName_BandID",
                table: "BandName",
                column: "BandID");

            migrationBuilder.CreateIndex(
                name: "IX_Election_BandID",
                table: "Election",
                column: "BandID");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionSong_SongID",
                table: "ElectionSong",
                column: "SongID");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionVote_AccountID",
                table: "ElectionVote",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionVote_SongID",
                table: "ElectionVote",
                column: "SongID");

            migrationBuilder.CreateIndex(
                name: "IX_Gig_BandID",
                table: "Gig",
                column: "BandID");

            migrationBuilder.CreateIndex(
                name: "IX_Gig_SetListID",
                table: "Gig",
                column: "SetListID");

            migrationBuilder.CreateIndex(
                name: "IX_Gig_VenueID",
                table: "Gig",
                column: "VenueID");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetRequests_AccountID",
                table: "PasswordResetRequests",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_AccountID",
                table: "Rating",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_SongID",
                table: "Rating",
                column: "SongID");

            migrationBuilder.CreateIndex(
                name: "IX_SetList_AccountID",
                table: "SetList",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_SetList_BandID",
                table: "SetList",
                column: "BandID");

            migrationBuilder.CreateIndex(
                name: "IX_SetListItem_SetListID",
                table: "SetListItem",
                column: "SetListID");

            migrationBuilder.CreateIndex(
                name: "IX_SetListItem_SongID",
                table: "SetListItem",
                column: "SongID");

            migrationBuilder.CreateIndex(
                name: "IX_Song_BandID",
                table: "Song",
                column: "BandID");

            migrationBuilder.CreateIndex(
                name: "IX_Song_CreatedByAccountID",
                table: "Song",
                column: "CreatedByAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Song_SongListTypeID",
                table: "Song",
                column: "SongListTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_SongAccount_AccountID",
                table: "SongAccount",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_SongListType_BandID",
                table: "SongListType",
                column: "BandID");

            migrationBuilder.CreateIndex(
                name: "IX_Venue_AddressID",
                table: "Venue",
                column: "AddressID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountCredential");

            migrationBuilder.DropTable(
                name: "AccountRole");

            migrationBuilder.DropTable(
                name: "BandAccount");

            migrationBuilder.DropTable(
                name: "BandName");

            migrationBuilder.DropTable(
                name: "ElectionSong");

            migrationBuilder.DropTable(
                name: "ElectionVote");

            migrationBuilder.DropTable(
                name: "Gig");

            migrationBuilder.DropTable(
                name: "PasswordResetRequests");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "SetListItem");

            migrationBuilder.DropTable(
                name: "SongAccount");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Election");

            migrationBuilder.DropTable(
                name: "Venue");

            migrationBuilder.DropTable(
                name: "SetList");

            migrationBuilder.DropTable(
                name: "Song");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "SongListType");

            migrationBuilder.DropTable(
                name: "Band");
        }
    }
}
