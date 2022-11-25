﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TSB.Portal.Backend.Infra.Repository;

#nullable disable

namespace TSB.Portal.Backend.Infra.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221124025101_lista-de-sinistros")]
    partial class listadesinistros
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TSB.Portal.Backend.Infra.Repository.Entities.Apolice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long?>("ClienteId")
                        .HasColumnType("bigint");

                    b.Property<long?>("CoberturaId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Emissao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Vigencia")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("CoberturaId");

                    b.ToTable("Apolices");
                });

            modelBuilder.Entity("TSB.Portal.Backend.Infra.Repository.Entities.Cliente", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("ChavePIX")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("EnderecoId")
                        .HasColumnType("bigint");

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId");

                    b.HasIndex("UserId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("TSB.Portal.Backend.Infra.Repository.Entities.Cobertura", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<decimal>("ValorCobertura")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Coberturas");
                });

            modelBuilder.Entity("TSB.Portal.Backend.Infra.Repository.Entities.Credential", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Credentials");
                });

            modelBuilder.Entity("TSB.Portal.Backend.Infra.Repository.Entities.Endereco", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Bairro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CEP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cidade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Complemento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rua")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("TSB.Portal.Backend.Infra.Repository.Entities.Funcionario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Cargo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Funcionarios");
                });

            modelBuilder.Entity("TSB.Portal.Backend.Infra.Repository.Entities.HistoricoPagamento", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long?>("ApoliceId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdPagamento")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApoliceId");

                    b.ToTable("HistoricoPagamentos");
                });

            modelBuilder.Entity("TSB.Portal.Backend.Infra.Repository.Entities.HistoricoSinistro", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long?>("ClienteId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Ocorrencia")
                        .HasColumnType("datetime2");

                    b.Property<long?>("SinistroId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("SinistroId");

                    b.ToTable("HistoricoSinistros");
                });

            modelBuilder.Entity("TSB.Portal.Backend.Infra.Repository.Entities.Sinistro", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long?>("CoberturaId")
                        .HasColumnType("bigint");

                    b.Property<string>("Tipo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ValorSinistro")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CoberturaId");

                    b.ToTable("Sinistros");
                });

            modelBuilder.Entity("TSB.Portal.Backend.Infra.Repository.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long?>("CredentialId")
                        .HasColumnType("bigint");

                    b.Property<string>("Document")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CredentialId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TSB.Portal.Backend.Infra.Repository.Entities.Apolice", b =>
                {
                    b.HasOne("TSB.Portal.Backend.Infra.Repository.Entities.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId");

                    b.HasOne("TSB.Portal.Backend.Infra.Repository.Entities.Cobertura", "Cobertura")
                        .WithMany()
                        .HasForeignKey("CoberturaId");

                    b.Navigation("Cliente");

                    b.Navigation("Cobertura");
                });

            modelBuilder.Entity("TSB.Portal.Backend.Infra.Repository.Entities.Cliente", b =>
                {
                    b.HasOne("TSB.Portal.Backend.Infra.Repository.Entities.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId");

                    b.HasOne("TSB.Portal.Backend.Infra.Repository.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Endereco");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TSB.Portal.Backend.Infra.Repository.Entities.Funcionario", b =>
                {
                    b.HasOne("TSB.Portal.Backend.Infra.Repository.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TSB.Portal.Backend.Infra.Repository.Entities.HistoricoPagamento", b =>
                {
                    b.HasOne("TSB.Portal.Backend.Infra.Repository.Entities.Apolice", "Apolice")
                        .WithMany()
                        .HasForeignKey("ApoliceId");

                    b.Navigation("Apolice");
                });

            modelBuilder.Entity("TSB.Portal.Backend.Infra.Repository.Entities.HistoricoSinistro", b =>
                {
                    b.HasOne("TSB.Portal.Backend.Infra.Repository.Entities.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId");

                    b.HasOne("TSB.Portal.Backend.Infra.Repository.Entities.Sinistro", "Sinistro")
                        .WithMany()
                        .HasForeignKey("SinistroId");

                    b.Navigation("Cliente");

                    b.Navigation("Sinistro");
                });

            modelBuilder.Entity("TSB.Portal.Backend.Infra.Repository.Entities.Sinistro", b =>
                {
                    b.HasOne("TSB.Portal.Backend.Infra.Repository.Entities.Cobertura", null)
                        .WithMany("Sinistros")
                        .HasForeignKey("CoberturaId");
                });

            modelBuilder.Entity("TSB.Portal.Backend.Infra.Repository.Entities.User", b =>
                {
                    b.HasOne("TSB.Portal.Backend.Infra.Repository.Entities.Credential", "Credential")
                        .WithMany()
                        .HasForeignKey("CredentialId");

                    b.Navigation("Credential");
                });

            modelBuilder.Entity("TSB.Portal.Backend.Infra.Repository.Entities.Cobertura", b =>
                {
                    b.Navigation("Sinistros");
                });
#pragma warning restore 612, 618
        }
    }
}