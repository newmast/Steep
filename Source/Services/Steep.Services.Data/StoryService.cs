﻿namespace Steep.Services.Data
{
    using System.Linq;
    using Contracts;
    using Steep.Data.Common;
    using Steep.Data.Models;
    using System.Collections.Generic;
    using System;

    public class StoryService : IStoryService
    {
        private IDbRepository<Story> storyRepository;
        private IDbRepository<Genre> genreRepository;

        public StoryService(
            IDbRepository<Story> storyRepository,
            IDbRepository<Genre> genreRepository)
        {
            this.storyRepository = storyRepository;
            this.genreRepository = genreRepository;
        }

        public Story Create(string storyName, string creatorId, IEnumerable<string> genreNames)
        {
            var existingGenres = this.genreRepository.All()
                .Where(x => genreNames.Contains(x.Name))
                .ToList();

            foreach (var genre in genreNames)
            {
                if (!existingGenres.AsQueryable().Select(x => x.Name).Contains(genre))
                {
                    var newGenre = new Genre
                    {
                        Name = genre
                    };

                    existingGenres.Add(newGenre);
                    this.genreRepository.Add(newGenre);
                }
            }

            this.genreRepository.Save();

            var story = new Story
            {
                AuthorId = creatorId,
                Name = storyName,
                Genres = existingGenres
            };

            this.storyRepository.Add(story);
            this.storyRepository.Save();

            return this.storyRepository.All()
                .FirstOrDefault(x => x.Name == story.Name);
        }

        public IQueryable<Story> All()
        {
            return this.storyRepository.All();
        }

        public IQueryable<Story> GetById(int id)
        {
            return this.storyRepository.All()
                .Where(x => x.Id == id);
        }

        public void IncreaseViewCount(int storyId)
        {
            var chapter = this.storyRepository.All().FirstOrDefault(x => x.Id == storyId);
            chapter.NumberOfViews++;
            this.storyRepository.Save();
        }

        public IQueryable<Story> AllForUser(string userId)
        {
            return this.storyRepository.All()
                .Where(x => x.AuthorId == userId);
        }

        public void Update(Story story)
        {
            var dbStory = this.storyRepository.All().FirstOrDefault(x => x.Id == story.Id);

            dbStory.IsDeleted = story.IsDeleted;
            dbStory.ModifiedOn = story.ModifiedOn;
            dbStory.Name = story.Name;
            dbStory.Genres = new List<Genre>(story.Genres);
            dbStory.Chapters = new List<Chapter>(story.Chapters);
            this.storyRepository.Save();
        }

        public IQueryable<Story> GetLastestStories(int numberOfStories)
        {
            return this.storyRepository.All()
                .OrderByDescending(x => x.CreatedOn)
                .Take(numberOfStories);
        }
    }
}
